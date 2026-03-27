using Entidades;
using System.Collections;
using System.Linq.Expressions;

namespace Aplicacao.Interface
{
    public interface IUsuarioAplicacao
    {
        Task AdicionarUsuario(Usuarios Objeto);
        Task AtualizarUsuario(Usuarios Objeto);
        Task DeletarUsuario(int id);
        Task<IEnumerable<Usuarios>> ListarUsuariosAdm(int id);
        Task<IEnumerable<Usuarios>> ListarUsuarios(Expression<Func<Usuarios, bool>> exUsuarios);
        Task<IEnumerable<Usuarios>> ListarUsuariosCustomizada(string idUsuario);
        Task<bool> ExisteUsuario(string email);
        Task<int> RetornarIdUsuario(string email);
        Task<string> RetornarTipoUsuario(string email);
        Task AtualizaToken(int idUsuario, string token);
        Task<Usuarios> RetornarUsuarioEmail(string email);
    }
}
