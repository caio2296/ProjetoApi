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
        public async Task<IEnumerable<FilterCat>> BuscarFiltros(int id)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("dbo.sp_MontaJsonComPai", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@IdPagina", SqlDbType.Int).Value = id;
                    command.Parameters.AddWithValue("@rootId", id);

                    var outputParam = new SqlParameter("@json", SqlDbType.NVarChar, -1)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputParam);

                    await conn.OpenAsync().ConfigureAwait(false);
                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);

                    var resultado = outputParam.Value?.ToString();

                    if (!string.IsNullOrWhiteSpace(resultado))
                    {
                        var filtro = JsonSerializer.Deserialize<FilterCat>(resultado);
                        return filtro != null ? new List<FilterCat> { filtro } : new List<FilterCat>();
                    }

                    return new List<FilterCat>();
                }
            }
            catch (JsonException ex)
            {
                throw new Exception("Erro ao desserializar os filtros.", ex);
            }
        }
    }
}
