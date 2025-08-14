using Aplicacao.Interface;
using Dominio.Interface;
using Entidades;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private ICalendarAplicacao _calendarAplicacao;
        public CalendarController(ICalendarAplicacao calendarAplicacao)
        {
            _calendarAplicacao = calendarAplicacao;
        }
        // GET: api/<FrutasController>
        [HttpGet("/api/BuscarCalendar")]
        [Produces("application/json")]
        public async Task<ActionResult<CalendarModel>> BuscarCalendar()
        {
            try
            {
                var calendar = await _calendarAplicacao.BuscarCalendar();
                if (calendar == null)
                {
                    return NotFound("Calendario não encontrado!");
                }
                return Ok(calendar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
