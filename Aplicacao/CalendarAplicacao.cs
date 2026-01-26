using Aplicacao.Interface;
using Dominio.Interface;
using Dominio.Servicos.Interfaces;
using Entidades;

namespace Aplicacao
{
    public class CalendarAplicacao : ICalendarAplicacao
    {
        private ICalendarService _calendar;
        public CalendarAplicacao(ICalendarService calendar)
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
            return await _calendar.BuscarCalendario();
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
