using Entidades.SendEmail;

namespace Dominio.Servicos.Interfaces
{
    public interface ISendEmailService
    {
        Task EnviarEmailAsync(EmailMessage message);
    }
}
