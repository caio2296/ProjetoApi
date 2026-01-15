using Dominio.Interface;
using Dominio.Servicos.Interfaces;
using Entidades.Filtros;

namespace Dominio.Servicos
{
    public class FiltrosServico : IFiltrosServicos
    {
        private IFiltros _filtros;

        public FiltrosServico(IFiltros filtros)
        {
                _filtros= filtros;
        }
        public async Task<FilterCat> BuscarId(int Id)
        {
            if (Id == null)
            {
                return null;
            }

            return await _filtros.BuscarFiltros(Id);
        }
    }
}
