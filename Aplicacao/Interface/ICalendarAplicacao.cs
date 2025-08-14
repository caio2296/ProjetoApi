using Aplicacao.Interface.Generico;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interface
{
    public interface ICalendarAplicacao : IGenericoAplicacao<CalendarModel>
    {
        Task<CalendarModel> BuscarCalendar();
    }
}
