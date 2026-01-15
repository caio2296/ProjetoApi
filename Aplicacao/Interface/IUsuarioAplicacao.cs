using Entidades;
using System.Linq.Expressions;

namespace Aplicacao.Interface
{
    public interface IUsuarioAplicacao
    {
        Task AdicionarUsuario(Usuarios Objeto);
        Task AtualizarUsuario(Usuarios Objeto);
        Task DeletarUsuario(int id);
        Task<List<Usuarios>> ListarUsuariosAdm(int id);
        Task<List<Usuarios>> ListarUsuarios(Expression<Func<Usuarios, bool>> exUsuarios);
        Task<List<Usuarios>> ListarUsuariosCustomizada(string idUsuario);
        Task<bool> ExisteUsuario(string email);
        Task<int> RetornarIdUsuario(string email);
        Task<string> RetornarTipoUsuario(string email);
        Task AtualizaToken(int idUsuario, string token);
    }
}
