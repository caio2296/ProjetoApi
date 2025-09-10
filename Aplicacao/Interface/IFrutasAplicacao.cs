using Aplicacao.Interface.Generico;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
