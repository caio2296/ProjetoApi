using Dominio.Interface;
using Entidades;
using Infraestrutura.Configuracao;
using Infraestrutura.Repositorio.Generico;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using System.Text.Json;

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

        public new async Task<List<Frutas>> ListarFrutas()
        {
            var lista = new List<Frutas>();
            string nomeProcedimento = "ListarFrutas";

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(nomeProcedimento, conn))
                {
                    await conn.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var fruta = new Frutas
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("SLT_ID")),
                                Descricao = reader.GetString(reader.GetOrdinal("SLT_Descricao")),
                                Cor = reader.GetString(reader.GetOrdinal("SLT_Cor")),
                                Tamanho = reader.GetString(reader.GetOrdinal("SLT_Tamanho"))
                            };

                            lista.Add(fruta);
                        }

                        return lista;

                    }
                }
            }
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

        public async Task<List<Frutas>> ListarFrutasEx(Expression<Func<Frutas, bool>> exFrutas)
        {
            using (var banco = new Contexto(_optionsBuilder))
            {
                return await banco.Frutas.Where(exFrutas).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Frutas>> ListarFrutasCustomizada(int idFrutas)
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
