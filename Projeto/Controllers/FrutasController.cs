using Dominio.Interface;
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
        private IFrutas _frutas;
        public FrutasController(IFrutas frutas)
        {
                _frutas= frutas;
        }
        // GET: api/<FrutasController>
        [HttpGet("/api/ListarFrutas")]
        [Produces("application/json")]
        public async Task<List<Frutas>> ListarFrutas()
        {
            return await _frutas.ListarFrutasSemEF();
        }

        // POST api/<FrutasController>
        [HttpPost("/api/AdicionarFrutas")]
        [Produces("application/json")]
        public async Task AdicionarFrutas([FromBody] Frutas novafruta)
        {
            await _frutas.AdicionarFrutasSemEF(novafruta);
        }

        // PUT api/<FrutasController>/5
        [HttpPut("/api/AtualizarFruta")]
        public async Task AtualizarFruta([FromBody] Frutas fruta)
        {
            var novaFruta = await _frutas.BuscarPorId(fruta.Id);
            novaFruta.Descricao = fruta.Descricao;
            novaFruta.Tamanho = fruta.Tamanho;
            novaFruta.Cor = fruta.Cor;

            //await _frutas.Atualizar(novaFruta);
            await _frutas.AtualizarFrutaSemEF(novaFruta);
        }

        // DELETE api/<FrutasController>/5
        [HttpDelete("/api/ExcluirFruta")]
        public async Task Delete([FromBody] Frutas fruta)
        {
            await _frutas.Excluir(fruta);
        }
    }
}
