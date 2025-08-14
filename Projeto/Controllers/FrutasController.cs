using Aplicacao.Interface;
using Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrutasController : ControllerBase
    {
        private IFrutasAplicacao _frutasAplicacao;
        public FrutasController(IFrutasAplicacao frutasAplicacao)
        {
            _frutasAplicacao = frutasAplicacao;
        }
        // GET: api/<FrutasController>
        [HttpGet("/api/ListarFrutas")]
        [Produces("application/json")]
        public async Task<ActionResult<List<Frutas>>> ListarFrutas()
        {
            try
            {
                return Ok(await _frutasAplicacao.ListarFrutasSemEF());
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
           
        }

        // POST api/<FrutasController>
        [HttpPost("/api/AdicionarFrutas")]
        [Produces("application/json")]
        public async Task<ActionResult> AdicionarFrutas([FromBody] Frutas novafruta)
        {
            try
            {
                await _frutasAplicacao.AdicionarFrutasSemEF(novafruta);
                return Ok();
            }  
            catch (Exception ex)
            {

               return StatusCode(500, ex.Message);
            }
          
        }

        // PUT api/<FrutasController>/5
        [HttpPut("/api/AtualizarFruta")]
        public async Task<ActionResult> AtualizarFruta([FromBody] Frutas fruta)
        {
            try
            {
                var novaFruta = await _frutasAplicacao.BuscarPorId(fruta.Id);
                novaFruta.Descricao = fruta.Descricao;
                novaFruta.Tamanho = fruta.Tamanho;
                novaFruta.Cor = fruta.Cor;

                //await _frutas.Atualizar(novaFruta);
                await _frutasAplicacao.AtualizarFrutaSemEF(novaFruta);
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }

        // DELETE api/<FrutasController>/5
        [HttpDelete("/api/ExcluirFruta")]
        public async Task<ActionResult> Delete([FromBody] Frutas fruta)
        {
            try
            {
                await _frutasAplicacao.DeletarFruta(fruta.Id);
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
    }
}
