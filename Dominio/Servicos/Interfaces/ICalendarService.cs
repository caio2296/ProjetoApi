using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos.Interfaces
{
    public interface ICalendarService
    {
        Task<CalendarModel> BuscarCalendario();
    }
}
