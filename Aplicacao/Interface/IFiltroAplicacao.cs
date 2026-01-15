using Aplicacao.Interface.Generico;
using Entidades.Filtros;

namespace Aplicacao.Interface
{
    public interface IFiltroAplicacao : IGenericoAplicacao<FilterCat>
    {
        Task<FilterCat> BuscarFiltros(int id);
    }
}
