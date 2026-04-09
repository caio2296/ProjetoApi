using Aplicacao.Interface;
using Dominio.Servicos.Interfaces;
using Entidades.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao
{
    public class TabelaAplicacao : ITabelaAplicacao
    {
        private readonly ITabelaServico _tabelaServico;

        public TabelaAplicacao(ITabelaServico tabelaServico)
        {
               _tabelaServico = tabelaServico;
        }
        public Task Adicionar(Root Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(Root Objeto)
        {
            throw new NotImplementedException();
        }

        public Task<Root> BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Root>> BuscarTabela(int id)
        {
            return await _tabelaServico.BuscarTabelaId(id);
        }

        public Task Excluir(Root Objeto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Root>> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
