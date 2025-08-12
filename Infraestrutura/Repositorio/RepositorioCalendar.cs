using Dominio.Interface;
using Entidades;
using Infraestrutura.Repositorio.Generico;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class RepositorioCalendar:RepositorioGenerico<CalendarModel>, ICalendar
    {
        private readonly string _connectionString;
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
                        calendar = JsonSerializer.Deserialize<CalendarModel>(json);
                    }
                    return calendar;
                }
            }
        }
    }
}
