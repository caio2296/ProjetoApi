using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interface
{
    public interface IEmailQueue
    {
        ValueTask EnqueueAsync(Usuarios usuario);
        ValueTask<Usuarios> DequeueAsync(CancellationToken cancellationToken);
    }
}
