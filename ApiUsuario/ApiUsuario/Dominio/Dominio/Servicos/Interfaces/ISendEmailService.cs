using Dominio.DTO;


namespace Dominio.Servicos.Interfaces
{
    public interface ISendEmailService
    {
        Task EnviarEmailAsync(EmailMessage message);
    }
}
