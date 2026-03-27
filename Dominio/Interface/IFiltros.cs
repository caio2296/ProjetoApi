using Dominio.Interface.Generico;
using Entidades.Filtros;

namespace Dominio.Interface
{
    public interface IFiltros: IGenerico<FilterCat>
    {
        Task<IEnumerable<FilterCat>>BuscarFiltros(int id);
    }
}
