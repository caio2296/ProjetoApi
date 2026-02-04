using Aplicacao.Interface;
using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projeto.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FrutasController : ControllerBase
    {
        private IFrutasAplicacao _frutasAplicacao;
        private readonly ILogger<FrutasController> _logger;
        public FrutasController(IFrutasAplicacao frutasAplicacao, ILogger<FrutasController> logger)
        {
            _frutasAplicacao = frutasAplicacao;
            _logger = logger;
        }
        // GET: api/<FrutasController>
        [HttpGet("/api/ListarFrutas")]
        [Produces("application/json")]
        public async Task<ActionResult<List<Frutas>>> ListarFrutas()
        {
            try
            {
                var frutas = await _frutasAplicacao.ListarFrutas();
                if (frutas == null || frutas.Count == 0)
                    return NotFound("Nenhuma fruta encontrada.");

                return Ok(frutas);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar as frutas!");
                return StatusCode(500, "Erro interno no servidor");
            }
           
        }

        // POST api/<FrutasController>
        [HttpPost("/api/AdicionarFrutas")]
        [Produces("application/json")]
        public async Task<ActionResult> AdicionarFrutas([FromBody] Frutas novafruta)
        {
            if (novafruta == null)
                return BadRequest("Objeto fruta não pode ser nulo.");

            try
            {
                await _frutasAplicacao.AdicionarFrutasSemEF(novafruta);
                return CreatedAtAction(nameof(ListarFrutas), new { id = novafruta.Id }, novafruta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar essa fruta!");
                return StatusCode(500, "Erro interno no servidor");
            }

        }

        // PUT api/<FrutasController>/5
        [HttpPut("/api/AtualizarFruta")]
        public async Task<ActionResult> AtualizarFruta([FromBody] Frutas fruta)
        {
            if (fruta == null || fruta.Id <= 0)
                return BadRequest("Objeto fruta inválido ou ID não informado.");

            try
            {
                var frutaExistente = await _frutasAplicacao.BuscarPorId(fruta.Id);
                if (frutaExistente == null)
                    return NotFound($"Fruta com ID {fruta.Id} não encontrada.");

                frutaExistente.Descricao = fruta.Descricao;
                frutaExistente.Tamanho = fruta.Tamanho;
                frutaExistente.Cor = fruta.Cor;

                await _frutasAplicacao.AtualizarFrutaSemEF(frutaExistente);
                return Ok(frutaExistente);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro nos argumentos");
                return BadRequest("Confira os argumentos usados para atualizar!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar!");
                return StatusCode(500, "Erro interno no servidor");
            }

        }

        // DELETE api/<FrutasController>/5
        [HttpDelete("/api/ExcluirFruta")]
        public async Task<ActionResult> Delete([FromBody] Frutas fruta)
        {
          if (fruta == null || fruta.Id <= 0)
                return BadRequest("Objeto fruta inválido ou ID não informado.");

            try
            {
                var frutaExistente = await _frutasAplicacao.BuscarPorId(fruta.Id);
                if (frutaExistente == null)
                    return NotFound($"Fruta com ID {fruta.Id} não encontrada.");

                await _frutasAplicacao.DeletarFruta(fruta.Id);
                return Ok("A Fruta selecionada foi deletada com sucesso!");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro nos argumentos");
                return BadRequest("Confira os argumentos usados para atualizar!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar essa fruta!");
                return StatusCode(500, "Erro interno no servidor");
            }

        }
    }
}
