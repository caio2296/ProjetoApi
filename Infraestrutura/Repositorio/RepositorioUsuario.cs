using Dominio.Interface;
using Entidades;
using Infraestrutura.Repositorio.Generico;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;

namespace Infraestrutura.Repositorio
{
    public class RepositorioUsuario : RepositorioGenerico<Usuarios>, IUsuario
    {
        private readonly string _connectionString;
        public RepositorioUsuario(string connectionString)
        {
                _connectionString = connectionString;
        }
        public async Task AdicionarUsuario(Usuarios usuario)
        {
            string nomeProcedimento = "AdicionarUsuario";

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(nomeProcedimento, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Email", usuario.Email);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task AtualizarUsuario(Usuarios usuario)
        {
            string nomeProcedimento = "AtualizarUsuario";

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(nomeProcedimento, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", usuario.Id);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@TokenJWT", usuario.TokenJWT);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeletarUsuario(int id)
        {
            const string nomeProcedimento = "DeletarUsuario";

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

        public async Task<bool> ExisteUsuario(string email)
        {
            Usuarios usuario = null;

            const string nomeProcedimento = "ExisteUsuario";

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(nomeProcedimento, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);

                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        return await reader.ReadAsync(); // true se encontrou, false se não
                    }
                }
            }
        }

        public async Task<int> RetornarIdUsuario(string email)
        {
            const string nomeProcedimento = "SelecionarUsuario";

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(nomeProcedimento, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);

                    await conn.OpenAsync();

                    var usuarioId = await cmd.ExecuteScalarAsync();
                    return int.Parse(usuarioId?.ToString()); // retorna null se não encontrar
                }
            }
        }
        public async Task AtualizarToken(int idUsuario, string token)
        {
            const string nomeProcedimento = "AtualizarToken";

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(nomeProcedimento, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", idUsuario);
                    cmd.Parameters.AddWithValue("@TokenJWT", token);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public Task<List<Usuarios>> ListarUsuarios(Expression<Func<Usuarios, bool>> exUsuarios)
        {
            throw new NotImplementedException();
        }

        public Task<List<Usuarios>> ListarUsuariosCustomizada(string idUsuario)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Usuarios>> ListarUsuariosAdm(int id)
        {
            var lista = new List<Usuarios>();
            const string nomeProcedimento = "ListarUsuariosExcetoId";

            using (var conn = new SqlConnection(_connectionString))
            {

                using (var cmd = new SqlCommand(nomeProcedimento, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var usuario = new Usuarios
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email"))
                                             ? string.Empty
            :                                  reader.GetString(reader.GetOrdinal("Email")),
                                UsuarioTipo = reader.GetString(reader.GetOrdinal("TipoUsuario"))
                            };

                            lista.Add(usuario);
                        }

                        return lista;

                    }
                }
            }
        }

        public async Task<string> RetornarTipoUsuario(string email)
        {
            const string nomeProcedimento = "RetornarTipoUsuario";

            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(nomeProcedimento, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);

                    await conn.OpenAsync();

                    var usuarioTipo = await cmd.ExecuteScalarAsync();
                    return usuarioTipo?.ToString(); // retorna null se não encontrar
                }
            }
        }

    }
}
