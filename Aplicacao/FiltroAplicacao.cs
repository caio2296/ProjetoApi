using Aplicacao.Interface;
using Dominio.Interface;
using Dominio.Servicos.Interfaces;
using Entidades.Filtros;

namespace Aplicacao
{
    public class FiltroAplicacao :IFiltroAplicacao
    {
        private IFiltrosServicos _filtros;
        public FiltroAplicacao(IFiltrosServicos filtros)
        {
            _filtros = filtros;
        }

        public async Task<FilterCat> BuscarFiltros(int id)
        {
            return await _filtros.BuscarId(id);
        }
        public Task Adicionar(FilterCat Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(FilterCat Objeto)
        {
            throw new NotImplementedException();
        }

        public Task<FilterCat> BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(FilterCat Objeto)
        {
            throw new NotImplementedException();
        }

        public Task<List<FilterCat>> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
