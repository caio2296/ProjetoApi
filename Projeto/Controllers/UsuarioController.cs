using Aplicacao.Interface;
using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Projeto.Middleware;
using Projeto.Model;
using Projeto.Token;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAplicacao _usuarioAplicacao;
        private readonly TokenJwtBuilder _tokenJwtBuilder;
        private readonly ILogger<UsuarioController> _logger;
        private readonly ISendEmailAplicacao _sendEmailAplicacao;


        private readonly IEmailQueue _emailQueue;
        public UsuarioController(IUsuarioAplicacao usuarioAplicacao, TokenJwtBuilder tokenJwtBuilder, ISendEmailAplicacao sendEmailAplicacao,
            IEmailQueue emailQueue, ILogger<UsuarioController> logger)
        {
            _usuarioAplicacao = usuarioAplicacao;
            _tokenJwtBuilder = tokenJwtBuilder;
            _sendEmailAplicacao = sendEmailAplicacao;
            _emailQueue = emailQueue;
            _logger = logger;
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [Produces("application/json")]
        [HttpPost("/api/RegistrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] LoginDto registro)
        {
            try
            {

                //throw new Exception("Erro forçado para teste");

                if (string.IsNullOrWhiteSpace(registro.Email))
                    return BadRequest("Falta alguns dados");
                if (await _usuarioAplicacao.ExisteUsuario(registro.Email))
                {
                    return Conflict("Usuário já cadastrado!");
                }

                Usuarios usuario = new Usuarios()
                {
                    Email = registro.Email,
                };


                await _usuarioAplicacao.AdicionarUsuario(usuario);

                await _emailQueue.EnqueueAsync(usuario);

                return Created("", new { message = "Usuário adicionado com sucesso!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar usuário");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [AllowAnonymous]
        [HttpPost("/api/CriarToken")]
        [Produces("application/json")]
        public async Task<ActionResult<string>> CriarToken([FromBody] LoginDto login)
        {
            try
            {
                //throw new Exception("Erro forçado para teste");

                if (string.IsNullOrWhiteSpace(login.Email))
                    return BadRequest("Falta alguns dados");

                //if (!await _usuarioAplicacao.ExisteUsuario(login.Email))
                //{
                //    return BadRequest("Não foi possivel encontrar o Usuario!");
                //}

                //var usuarioId = await _usuarioAplicacao.RetornarIdUsuario(login.Email);
                //var UsuarioTipo = await _usuarioAplicacao.RetornarTipoUsuario(login.Email);
                //if (usuarioId == null || UsuarioTipo == null)
                //    return Unauthorized(" Usuario não autorizado, verifique seu email!"); 

                var usuario = await _usuarioAplicacao.RetornarUsuarioEmail(login.Email);

                if (usuario == null)
                    return BadRequest("Não foi possivel encontrar o Usuario!");

                if (usuario.Id.ToString() == null || usuario.UsuarioTipo == null)
                    return Unauthorized(" Usuario não autorizado, verifique seu email!");

                //var token = _tokenJwtBuilder.GerarTokenJwt(usuarioId.ToString(),UsuarioTipo, login.Email);

                var token = _tokenJwtBuilder.GerarTokenJwt(usuario.Id.ToString(), usuario.UsuarioTipo, login.Email);

                await _usuarioAplicacao.AtualizaToken(usuario.Id, token.value);
                //token.value
                return Ok(token.value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao acessar usuario!");
                return StatusCode(500, "Erro interno no servidor");
            }

        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpDelete("/api/DeleteUsuario")]
        public async Task<IActionResult> DeleteUsuario([FromBody] int id)
        {
            try
            {
                //throw new Exception("Erro forçado para teste");

                if (id <= 0)
                    return BadRequest("Id inválido!");

                await _usuarioAplicacao.DeletarUsuario(id);
                return Ok("Usuário deletado com sucesso!");
            }
            catch (SqlException ex)
            {
                // por exemplo, se o ID não existe
                return NotFound($"Erro ao deletar usuário: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar usuario!");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpGet("/api/ListarUsuario")]
        public async Task<ActionResult<List<UsuarioDto>>> ListarUsario()
        {
            try
            {
                //throw new Exception("Erro forçado para teste");

                // recupera o ID do usuário logado
                var userId = User.FindFirst("idUsuario")?.Value;


                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Usuário não autenticado");

                // chama a aplicação passando o id
                var usuarios = await _usuarioAplicacao.ListarUsuariosAdm(int.Parse(userId));

                List<UsuarioDto> usuariosDto = usuarios.Select(u => (UsuarioDto)u).ToList();
                return Ok(usuariosDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar usuarios !");
                return StatusCode(500, "Erro interno no servidor");
            }
        }
    }
}
