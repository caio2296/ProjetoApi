using Dominio.Interface.Generico;
using Entidades;
using System.Linq.Expressions;

namespace Dominio.Interface
{
    public interface IUsuario:IGenerico<Usuarios>
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
        Task AtualizarToken(int idUsuario, string token);

        Task<Usuarios> RetornarUsuarioEmail(string email);
    }
}
