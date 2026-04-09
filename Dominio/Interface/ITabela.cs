using Dominio.Interface.Generico;
using Entidades.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface
{
    public interface ITabela:IGenerico<Root>
    {
        Task<IEnumerable<Root>> BuscarTabela(int id);
    }
}
