using Dominio.Interface;
using Entidades;
using Infraestrutura.Repositorio.Generico;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;

namespace Infraestrutura.Repositorio
{
    public class RepositorioCalendar:RepositorioGenerico<CalendarModel>, ICalendar
    {
        private readonly string _connectionString;

        private static readonly JsonSerializerOptions _jsonOptions =
                                        new()
                                        {
                                            PropertyNameCaseInsensitive = true
                                        };

        public RepositorioCalendar(string connection)
        {
            _connectionString = connection;
        }

        public  async Task<CalendarModel> BuscarCalendar()
        {
            var calendar = new CalendarModel();
            string nomeProcedimento = "GetCalendario";


            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(nomeProcedimento, conn))
                {
                    await conn.OpenAsync();

                    cmd.CommandType = CommandType.StoredProcedure;

                    var resultado = await cmd.ExecuteScalarAsync();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        string json = Convert.ToString(resultado);
                        calendar = JsonSerializer.Deserialize<CalendarModel>(json, _jsonOptions);
                    }
                    return calendar;
                }
            }
        }
    }
}
