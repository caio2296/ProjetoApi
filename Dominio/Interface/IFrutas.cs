using Dominio.Interface.Generico;
using Entidades;
using System.Linq.Expressions;

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
