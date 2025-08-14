using Aplicacao.Interface;
using Dominio.Interface;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao
{
    public class FrutasAplicacao : IFrutasAplicacao
    {
        private IFrutas _frutas;
        public FrutasAplicacao(IFrutas frutas)
        {
            _frutas = frutas;
        }
        public Task Adicionar(Frutas Objeto)
        {
            throw new NotImplementedException();
        }

        public async Task AdicionarFrutasSemEF(Frutas novafruta)
        {
             await _frutas.AdicionarFrutasSemEF(novafruta);
        }

        public Task Atualizar(Frutas Objeto)
        {
            throw new NotImplementedException();
        }

        public async Task AtualizarFrutaSemEF(Frutas Objeto)
        {
            await _frutas.AtualizarFrutaSemEF(Objeto);
        }

        public async Task<Frutas> BuscarPorId(string id)
        {
            var fruta = await _frutas.BuscarPorId(id);
            if (fruta == null)
                throw new InvalidOperationException("Fruta não encontrada");
            return fruta;
        }

        public async Task DeletarFruta(string id)
        {
            await _frutas.DeletarFruta(id);
        }

        public Task Excluir(Frutas Objeto)
        {
            throw new NotImplementedException();
        }

        public Task<List<Frutas>> Listar()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Frutas>> ListarFrutasSemEF()
        {
            return await _frutas.ListarFrutasSemEF();
        }
    }
}
