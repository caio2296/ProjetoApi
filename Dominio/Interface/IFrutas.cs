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
        Task<IReadOnlyCollection<Frutas>> ListarFrutas();
        Task<IReadOnlyCollection<Frutas>> ListarFrutasEx(Expression<Func<Frutas, bool>> exFrutas);
        Task<IReadOnlyCollection<Frutas>> ListarFrutasCustomizada(int idUsuario);
        Task<bool> ExisteFrutas(int idFrutas);
    }
}
