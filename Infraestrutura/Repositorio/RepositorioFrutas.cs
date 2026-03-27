using Dominio.Interface;
using Entidades;
using Infraestrutura.Configuracao;
using Infraestrutura.Repositorio.Generico;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace Infraestrutura.Repositorio
{
    public class RepositorioFrutas:RepositorioGenerico<Frutas>, IFrutas
    {
        private readonly DbContextOptions<Contexto> _optionsBuilder;

        private readonly string _connectionString;

        public RepositorioFrutas(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> ExisteFrutas(int id)
        {
            using (var banco = new Contexto(_optionsBuilder))
            {
                return await banco.Frutas
                       .Where(f => f.Id.Equals(id))
                       .AsTracking()
                       .AnyAsync();
            }
        }

        public new async Task AdicionarFruta(Frutas fruta)
        {
            string nomeProcedimento = "AdicionarFruta";

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(nomeProcedimento, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Descricao", fruta.Descricao);
                    cmd.Parameters.AddWithValue("@Tamanho", fruta.Tamanho);
                    cmd.Parameters.AddWithValue("@Cor", fruta.Cor);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public new async Task AtualizarFruta(Frutas fruta)
        {
            string nomeProcedimento = "AtualizarFruta";

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(nomeProcedimento, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", fruta.Id);
                    cmd.Parameters.AddWithValue("@Descricao", fruta.Descricao);
                    cmd.Parameters.AddWithValue("@Tamanho", fruta.Tamanho);
                    cmd.Parameters.AddWithValue("@Cor", fruta.Cor);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }

        }

        // Adicionar a lista de fruta e byid sem o ef 

        public async Task<IReadOnlyCollection<Frutas>> ListarFrutas()
        {
            var lista = new List<Frutas>();
            const string nomeProcedimento = "ListarFrutas";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(nomeProcedimento, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            await conn.OpenAsync().ConfigureAwait(false);

            using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);

            // 🔥 Cache dos ordinais (performance)
            var ordId = reader.GetOrdinal("SLT_ID");
            var ordDescricao = reader.GetOrdinal("SLT_Descricao");
            var ordCor = reader.GetOrdinal("SLT_Cor");
            var ordTamanho = reader.GetOrdinal("SLT_Tamanho");

            while (await reader.ReadAsync().ConfigureAwait(false))
            {
                var fruta = new Frutas
                {
                    Id = reader.GetInt32(ordId),
                    Descricao = reader.IsDBNull(ordDescricao) ? null : reader.GetString(ordDescricao),
                    Cor = reader.IsDBNull(ordCor) ? null : reader.GetString(ordCor),
                    Tamanho = reader.IsDBNull(ordTamanho) ? null : reader.GetString(ordTamanho)
                };

                lista.Add(fruta);
            }

            return lista;
        }

        public new async Task<Frutas?> BuscarPorId(int id)
        {
            const string sql = "SELECT SLT_ID, SLT_Descricao, SLT_Tamanho, SLT_Cor FROM Frutas WHERE SLT_ID = @Id";

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    await conn.OpenAsync();
                    using var reader = await cmd.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        return new Frutas
                        {
                            Id = int.Parse(reader["SLT_ID"].ToString()),
                            Descricao = reader["SLT_Descricao"].ToString(),
                            Tamanho = reader["SLT_Tamanho"].ToString(),
                            Cor = reader["SLT_Cor"].ToString()
                        };
                    }
                    return null;
                }
            }  
        }

        public async Task DeletarFruta(int id)
        {
            const string nomeProcedimento = "DeletarFruta";

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(nomeProcedimento, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IReadOnlyCollection<Frutas>> ListarFrutasEx(Expression<Func<Frutas, bool>> exFrutas)
        {
            using (var banco = new Contexto(_optionsBuilder))
            {
                return await banco.Frutas.Where(exFrutas).AsNoTracking().ToListAsync();
            }
        }

        public async Task<IReadOnlyCollection<Frutas>> ListarFrutasCustomizada(int idFrutas)
        {
            using (var banco = new Contexto(_optionsBuilder))
            {
                var listaFrutas = (from Frutas in banco.Frutas
                                     where Frutas.Id == idFrutas
                                     select new Frutas
                                     {
                                         Id = Frutas.Id,
                                         Descricao = Frutas.Descricao,
                                         Tamanho = Frutas.Tamanho,
                                         Cor = Frutas.Cor,

                                     }).AsNoTracking().ToListAsync();
                return await listaFrutas;
            }
        }
    }
}
