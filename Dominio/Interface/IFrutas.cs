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
        Task AdicionarFrutasSemEF(Frutas Objeto);
        Task AtualizarFrutaSemEF(Frutas Objeto);
        Task DeletarFruta(int id);
        Task<List<Frutas>> ListarFrutasSemEF();
        Task<List<Frutas>> ListarFrutas(Expression<Func<Frutas, bool>> exFrutas);
        Task<List<Frutas>> ListarFrutasCustomizada(int idUsuario);
        Task<bool> ExisteFrutas(int idFrutas);
    }
}
