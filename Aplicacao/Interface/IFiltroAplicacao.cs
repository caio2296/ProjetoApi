using Aplicacao.Interface.Generico;
using Entidades.Filtros;

namespace Aplicacao.Interface
{
    public interface IFiltroAplicacao : IGenericoAplicacao<FilterCat>
    {
        Task<List<FilterCat>> BuscarFiltros(int id);
    }
}
