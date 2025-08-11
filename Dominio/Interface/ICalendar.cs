using Dominio.Interface.Generico;
using Entidades;
using System.Linq.Expressions;

namespace Dominio.Interface
{
    public interface ICalendar:IGenerico<CalendarModel>
    {
        Task<CalendarModel> BuscarCalendar();
    }
}
