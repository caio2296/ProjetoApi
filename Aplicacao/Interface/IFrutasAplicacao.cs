using Aplicacao.Interface.Generico;
using Entidades;

namespace Aplicacao.Interface
{
    public interface IFrutasAplicacao : IGenericoAplicacao<Frutas>
    {
        Task AdicionarFrutasSemEF(Frutas Objeto);
        Task AtualizarFrutaSemEF(Frutas Objeto);
        Task DeletarFruta(int id);
        Task<List<Frutas>> ListarFrutas();
    }
}
