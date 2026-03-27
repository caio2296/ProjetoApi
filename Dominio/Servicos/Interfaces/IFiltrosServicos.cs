using Entidades.Filtros;

namespace Dominio.Servicos.Interfaces
{
    public interface IFiltrosServicos
    {
        Task<FilterCat> BuscarId(int Id); 
    }
}
