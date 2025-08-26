using Entidades;

namespace Dominio.Servicos.Interfaces
{
    public interface IFrutasServicos
    {
        Task AdicionarFrutasSemEF(Frutas frutas);
        Task AtualizarFrutaSemEF(Frutas frutas);
        Task<Frutas> BuscarPorId(int id);
        Task<List<Frutas>> ListarFrutasSemEF();
        Task DeletarFruta(int id);
    }
}
