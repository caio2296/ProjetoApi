using Aplicacao.Interface;
using Dominio.Interface;
using Entidades;
using System.Linq.Expressions;

namespace Aplicacao
{
    public class UsuarioAplicacao : IUsuarioAplicacao
    {
        private IUsuario _usuario;
        public UsuarioAplicacao(IUsuario usuario)
        {
                _usuario = usuario;
        }
        public async Task AdicionarUsuarioSemEF(Usuarios Objeto)
        {
           await _usuario.AdicionarUsuarioSemEF(Objeto);
        }

        public async Task AtualizarUsuarioSemEF(Usuarios Objeto)
        {
            await _usuario.AtualizarUsuarioSemEF(Objeto);
        }

        public async Task DeletarUsuario(int id)
        {
            await _usuario.DeletarUsuario(id);
        }

        public async Task<bool> ExisteUsuario(string email)
        {
            return await _usuario.ExisteUsuario(email);
        }

        public Task<List<Usuarios>> ListarUsuarios(Expression<Func<Usuarios, bool>> exUsuarios)
        {
            throw new NotImplementedException();
        }

        public Task<List<Usuarios>> ListarUsuariosCustomizada(string idUsuario)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Usuarios>> ListarUsuariosSemEF(int id)
        {
            return await _usuario.ListarUsuariosSemEF(id); 
        }

        public async Task<int> RetornarIdUsuario(string email)
        {
            return await _usuario.RetornarIdUsuario(email);
        }

        public async Task  AtualizaToken(int idUsuario, string token)
        {
            await _usuario.AtualizarToken(idUsuario,token);
        }

        public Task<string> RetornarTipoUsuario(string email)
        {
            return _usuario.RetornarTipoUsuario(email);
        }
    }
}
