using Aplicacao.Interface.Generico;
using Entidades;

namespace Aplicacao.Interface
{
    public interface ICalendarAplicacao : IGenericoAplicacao<CalendarModel>
    {
        Task<CalendarModel> BuscarCalendar();
    }
}
