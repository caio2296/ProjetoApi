using Aplicacao.Interface;
using Entidades;
using System.Threading.Channels;

namespace Aplicacao
{
    public class EmailQueue : IEmailQueue
    {
        private readonly Channel<Usuarios> _queue;

        public EmailQueue()
        {
            _queue = Channel.CreateUnbounded<Usuarios>();
        }

        public async ValueTask EnqueueAsync(Usuarios usuario)
        {
            await _queue.Writer.WriteAsync(usuario);
        }

        public async ValueTask<Usuarios> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}
