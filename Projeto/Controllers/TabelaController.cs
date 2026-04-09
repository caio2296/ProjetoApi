using Aplicacao;
using Aplicacao.Interface;
using Entidades.Filtros;
using Entidades.Tabelas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TabelaController : ControllerBase
    {
        private readonly ITabelaAplicacao _tabelaAplicacao;

        public TabelaController(ITabelaAplicacao tabelaAplicacao)
        {
                _tabelaAplicacao = tabelaAplicacao;
        }

        [HttpGet("/api/BuscarTabela/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Root>>> BuscarTabela(int id)
        {
            try
            {
                var tabela = await _tabelaAplicacao.BuscarTabela(id);
                if (tabela == null)
                {
                    return NotFound("Tabela não encontrada!");
                }
                return Ok(tabela);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
