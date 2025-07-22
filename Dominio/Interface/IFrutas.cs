using Dominio.Interface.Generico;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface
{
    public interface IFrutas: IGenerico<Frutas>
    {
        Task<List<Frutas>> ListarFrutas(Expression<Func<Frutas, bool>> exFrutas);
        Task<List<Frutas>> ListarFrutasCustomizada(string idUsuario);
        Task<bool> ExisteFrutas(string idFrutas);
    }
}
