namespace Dominio.Interface.Generico
{
    public interface IGenerico<T> where T : class
    {
        Task Adicionar(T Objeto);
        Task Atualizar(T Objecto);
        Task Excluir(T Objeto);
        Task<T?> BuscarPorId(int id);
        Task<List<T>> Listar();
    }
}
