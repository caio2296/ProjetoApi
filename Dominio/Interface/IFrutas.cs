using Dominio.Interface.Generico;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface
{
    public interface IFrutas: IGenerico<Frutas>
    {
        Task AdicionarFruta(Frutas Objeto);
        Task AtualizarFruta(Frutas Objeto);
        Task DeletarFruta(int id);
        Task<List<Frutas>> ListarFrutas();
        Task<List<Frutas>> ListarFrutasEx(Expression<Func<Frutas, bool>> exFrutas);
        Task<List<Frutas>> ListarFrutasCustomizada(int idUsuario);
        Task<bool> ExisteFrutas(int idFrutas);
    }
}
