using Dominio.Interface.Generico;
using Entidades;

namespace Dominio.Interface
{
    public interface ICalendar:IGenerico<CalendarModel>
    {
        Task<CalendarModel> BuscarCalendar();
    }
}
