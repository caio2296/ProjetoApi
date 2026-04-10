using Entidades.Filtros;

namespace Dominio.Servicos.Interfaces
{
    public interface IFiltrosServicos
    {
        Task<IEnumerable<FilterCat>> BuscarId(int Id); 
    }
}
