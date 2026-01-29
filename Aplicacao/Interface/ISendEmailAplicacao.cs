using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interface
{
    public interface ISendEmailAplicacao
    {
        Task EnviarEmailAsync(Usuarios registro);
    }
}
