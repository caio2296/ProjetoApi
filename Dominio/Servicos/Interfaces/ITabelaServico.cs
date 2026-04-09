using Entidades.Tabelas;

namespace Dominio.Servicos.Interfaces
{
    public interface ITabelaServico
    {
        Task<IEnumerable<Root>> BuscarTabelaId(int id);
    }
}
