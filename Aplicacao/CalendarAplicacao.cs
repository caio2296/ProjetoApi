using Aplicacao.Interface;
using Dominio.Interface;
using Entidades;

namespace Aplicacao
{
    public class CalendarAplicacao : ICalendarAplicacao
    {
        private ICalendar _calendar;
        public CalendarAplicacao(ICalendar calendar)
        {
            _calendar = calendar;
        }
        public Task Adicionar(CalendarModel Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(CalendarModel Objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<CalendarModel> BuscarCalendar()
        {
            return await _calendar.BuscarCalendar();
        }

        public Task<CalendarModel> BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(CalendarModel Objeto)
        {
            throw new NotImplementedException();
        }

        public Task<List<CalendarModel>> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
