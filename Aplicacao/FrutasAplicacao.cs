using Aplicacao.Interface;
using Dominio.Servicos.Interfaces;
using Entidades;

namespace Aplicacao
{
    public class FrutasAplicacao : IFrutasAplicacao
    {

        private IFrutasServicos _frutasServicos;
        public FrutasAplicacao(IFrutasServicos frutasServicos)
        {
            _frutasServicos = frutasServicos;
        }
        public Task Adicionar(Frutas Objeto)
        {
            throw new NotImplementedException();
        }

        public async Task AdicionarFrutasSemEF(Frutas novafruta)
        {
             await _frutasServicos.AdicionarFrutasSemEF(novafruta);
        }

        public Task Atualizar(Frutas Objeto)
        {
            throw new NotImplementedException();
        }

        public async Task AtualizarFrutaSemEF(Frutas Objeto)
        {
            await _frutasServicos.AtualizarFrutaSemEF(Objeto);
        }

        public async Task<Frutas> BuscarPorId(string id)
        {
            var fruta = await _frutasServicos.BuscarPorId(id);
            if (fruta == null)
                throw new InvalidOperationException("Fruta não encontrada");
            return fruta;
        }

        public async Task DeletarFruta(string id)
        {
            await _frutasServicos.DeletarFruta(id);
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
            return await _frutasServicos.ListarFrutasSemEF();
        }
    }
}
