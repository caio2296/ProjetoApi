using Dominio.Interface.Generico;
using Entidades.Filtros;

namespace Dominio.Interface
{
    public interface IFiltros: IGenerico<FilterCat>
    {
        Task<FilterCat>BuscarFiltros(int id);
    }
}
