using Dominio.DTO;
using Microsoft.Extensions.Logging;

namespace Infraestrutura.SendEmail.Interface
{
    public interface IEmailProducer
    {
        Task TentarEnviarEmailAsync(EmailMessage mensagem);
        Task EnviarAsync(EmailMessage mensagem);
    }
}
