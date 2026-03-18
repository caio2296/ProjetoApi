using Entidades;

namespace Dominio.Servicos.Interfaces
{
    public interface IUsuarioServico
    {
        Task AdicionarUsarioSemEF(Usuarios usuarios);
        Task AtualizarUsuarioSemEF(Usuarios usuarios);
        Task<Usuarios> BuscarPorId(int id);
        Task<List<Usuarios>> ListarUsuariosSemEF(int id);
        Task DeletarUsuario(int id);
        Task<bool> ExisteUsuario(string email);
        Task<List<Usuarios>> ListarUsuariosAdm(int id);
        Task<int> RetornarIdUsuario(string email);
        Task AtualizaToken(int idUsuario, string token);
        Task<string> RetornarTipoUsuario(string email);
        Task<Usuarios> RetornarUsuarioEmail(string email);
    }
}
