using Aplicacao.Interface;
using Dominio.Interface;
using Dominio.Servicos.Interfaces;
using Entidades;
using System.Linq.Expressions;

namespace Aplicacao
{
    public class UsuarioAplicacao : IUsuarioAplicacao
    {
        private IUsuarioServico _usuario;
        public UsuarioAplicacao(IUsuarioServico usuario)
        {
                _usuario = usuario;
        }
        public async Task AdicionarUsuario(Usuarios Objeto)
        {
           await _usuario.AdicionarUsarioSemEF(Objeto);
        }

        public async Task AtualizarUsuario(Usuarios Objeto)
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

        public async Task<List<Usuarios>> ListarUsuariosAdm(int id)
        {
            return await _usuario.ListarUsuariosAdm(id); 
        }

        public async Task<int> RetornarIdUsuario(string email)
        {
            return await _usuario.RetornarIdUsuario(email);
        }

        public async Task  AtualizaToken(int idUsuario, string token)
        {
            await _usuario.AtualizaToken(idUsuario,token);
        }

        public Task<string> RetornarTipoUsuario(string email)
        {
            return _usuario.RetornarTipoUsuario(email);
        }

        public Task<Usuarios> RetornarUsuarioEmail(string email)
        {
            return _usuario.RetornarUsuarioEmail(email);
        }

    }
}
