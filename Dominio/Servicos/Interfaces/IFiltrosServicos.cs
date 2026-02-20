using Entidades.Filtros;

namespace Dominio.Servicos.Interfaces
{
    public interface IFiltrosServicos
    {
        Task<List<FilterCat>> BuscarId(int Id); 
    }
}
