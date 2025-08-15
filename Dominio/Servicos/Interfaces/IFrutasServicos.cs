using Entidades;

namespace Dominio.Servicos.Interfaces
{
    public interface IFrutasServicos
    {
        Task AdicionarFrutasSemEF(Frutas frutas);
        Task AtualizarFrutaSemEF(Frutas frutas);
        Task<Frutas> BuscarPorId(string id);
        Task<List<Frutas>> ListarFrutasSemEF();
        Task DeletarFruta(string id);
    }
}
