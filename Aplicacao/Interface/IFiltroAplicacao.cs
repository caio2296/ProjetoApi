using Aplicacao.Interface.Generico;
using Entidades.Filtros;

namespace Aplicacao.Interface
{
    public interface IFiltroAplicacao : IGenericoAplicacao<FilterCat>
    {
        Task<IEnumerable<FilterCat>> BuscarFiltros(int id);
    }
}
