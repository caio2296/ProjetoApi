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
        public async Task<IReadOnlyCollection<FilterCat>> BuscarFiltros(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_MontaJsonPorPagina", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@IdPagina", SqlDbType.Int).Value = id;

            var outputParam = new SqlParameter("@JsonFinal", SqlDbType.NVarChar, -1)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(outputParam);

            await conn.OpenAsync().ConfigureAwait(false);
            await command.ExecuteNonQueryAsync().ConfigureAwait(false);

            var resultado = outputParam.Value?.ToString();

            if (string.IsNullOrWhiteSpace(resultado))
                return Array.Empty<FilterCat>();

            try
            {
                var filtros = JsonSerializer.Deserialize<List<FilterCat>>(resultado);

                return filtros ?? new List<FilterCat>();
            }
            catch (JsonException ex)
            {
                throw new Exception("Erro ao desserializar os filtros.", ex);
            }
        }
    }
}
