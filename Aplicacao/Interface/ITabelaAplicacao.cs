using Aplicacao.Interface.Generico;
using Entidades.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interface
{
    public interface ITabelaAplicacao:IGenericoAplicacao<Root>
    {
        Task<IEnumerable<Root>> BuscarTabela(int id);
    }
}
