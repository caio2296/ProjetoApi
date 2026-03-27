using Aplicacao.Interface;
using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CalendarController : ControllerBase
    {
        private ICalendarAplicacao _calendarAplicacao;
        private readonly ILogger<CalendarController> _logger;
        public CalendarController(ICalendarAplicacao calendarAplicacao, ILogger<CalendarController> logger)
        {
            _calendarAplicacao = calendarAplicacao;
            _logger = logger;
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
                    return NotFound("Calendário não encontrado!");
                }
                return Ok(calendar);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar calendário!");
                return StatusCode(500, "Erro interno no servidor");
            }
        }
    }
}
