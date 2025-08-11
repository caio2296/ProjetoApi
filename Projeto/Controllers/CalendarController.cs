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
        private ICalendar _calendar;
        public CalendarController(ICalendar calendar)
        {
            _calendar = calendar;
        }
        // GET: api/<FrutasController>
        [HttpGet("/api/BuscarCalendar")]
        [Produces("application/json")]
        public async Task<ActionResult<CalendarModel>> BuscarCalendar()
        {

            return await _calendar.BuscarCalendar();
        }

        // GET api/<CalendarController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CalendarController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CalendarController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CalendarController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
