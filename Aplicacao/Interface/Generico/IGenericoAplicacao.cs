using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interface.Generico
{
    public interface IGenericoAplicacao<T> where T: class
    {
        Task Adicionar(T Objeto);
        Task Atualizar(T Objeto);
        Task Excluir(T Objeto);
        Task<T> BuscarPorId(string id);
        Task<List<T>> Listar();
    }
}
