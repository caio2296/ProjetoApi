using Dominio.Interface;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos.Interfaces
{
    public interface IUsuarioServico
    {
        Task AdicionarUsarioSemEF(Usuarios usuarios);
        Task AtualizarUsuarioSemEF(Usuarios usuarios);
        Task<Usuarios> BuscarPorId(int id);
        Task<List<Usuarios>> ListarUsuariosSemEF(int id);
        Task DeletarUsuario(int id);
    }
}
