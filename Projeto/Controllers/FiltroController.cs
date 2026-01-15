using Aplicacao.Interface;
using Entidades.Filtros;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FiltroController : ControllerBase
    {
        private IFiltroAplicacao _filtroAplicacao;
        public FiltroController(IFiltroAplicacao filtroAplicacao)
        {
            _filtroAplicacao = filtroAplicacao;
        }
        // GET: api/<FrutasController>
        [HttpGet("/api/BuscarFiltro/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<FilterCat>> BuscarFiltro(int id)
        {
            try
            {
                var filtro = await _filtroAplicacao.BuscarFiltros(id);
                if (filtro == null)
                {
                    return NotFound("Filtro não encontrado!");
                }
                return Ok(filtro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
