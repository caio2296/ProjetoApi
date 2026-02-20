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
        public async Task<List<FilterCat>> BuscarFiltros(int id)
        {
            var filtro = new List<FilterCat>();
            using (var conn = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("dbo.sp_MontaJsonPorPagina", conn))
            {
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.Add("@IdPagina", SqlDbType.Int).Value = id;

                var outputParam = new SqlParameter("@JsonFinal", SqlDbType.NVarChar, -1)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParam);

                await conn.OpenAsync();
                await command.ExecuteNonQueryAsync();

                var resultado = outputParam.Value?.ToString();
                if (!string.IsNullOrWhiteSpace(resultado))
                {
                    filtro = JsonSerializer.Deserialize<List<FilterCat>>(resultado);


                }

                return filtro;

            }
        }
    }
}
