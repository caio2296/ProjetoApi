using Dominio.Interface;
using Dominio.Servicos.Interfaces;
using Entidades.Tabelas;

namespace Dominio.Servicos
{
    public class TabelaServico : ITabelaServico
    {
        private ITabela _tabela;

        public TabelaServico(ITabela tabela)
        {
                _tabela = tabela;   
        }
        public Task<IEnumerable<Root>> BuscarTabelaId(int id)
        {
            if (id== null)
            {
                return null;
            }

            return _tabela.BuscarTabela(id);
        }
    }
}
