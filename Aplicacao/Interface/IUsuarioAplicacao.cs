using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interface
{
    public interface IUsuarioAplicacao
    {
        Task AdicionarUsuarioSemEF(Usuarios Objeto);
        Task AtualizarUsuarioSemEF(Usuarios Objeto);
        Task DeletarUsuario(int id);
        Task<List<Usuarios>> ListarUsuariosSemEF(int id);
        Task<List<Usuarios>> ListarUsuarios(Expression<Func<Usuarios, bool>> exUsuarios);
        Task<List<Usuarios>> ListarUsuariosCustomizada(string idUsuario);
        Task<bool> ExisteUsuario(string email);
        Task<int> RetornarIdUsuario(string email);
        Task<string> RetornarTipoUsuario(string email);
        Task AtualizaToken(int idUsuario, string token);
    }
}
