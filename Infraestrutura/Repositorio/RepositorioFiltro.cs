using Dominio.Interface;
using Entidades.Filtros;
using Infraestrutura.Repositorio.Generico;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;

namespace Infraestrutura.Repositorio
{
    public class RepositorioFiltro : RepositorioGenerico<FilterCat>, IFiltros
    {
        private readonly string _connectionString;
        public RepositorioFiltro(string connection)
        {
            _connectionString = connection;
        }
        public async Task<FilterCat> BuscarFiltros(int id)
        {
            var filtro = new FilterCat();
            using (var conn = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("dbo.sp_MontaJsonComPai", conn))
            {
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.AddWithValue("@rootId", id);

                var outputParam = new SqlParameter("@json", SqlDbType.NVarChar, -1)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParam);

                await conn.OpenAsync();
                await command.ExecuteNonQueryAsync();

                var resultado = outputParam.Value?.ToString();
                if (!string.IsNullOrWhiteSpace(resultado))
                {
                    filtro = JsonSerializer.Deserialize<FilterCat>(resultado);


                }

                return filtro;

            }
        }
    }
}
