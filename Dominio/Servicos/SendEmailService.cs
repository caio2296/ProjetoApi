using Dominio.Interface;
using Dominio.Servicos.Interfaces;
using Entidades.SendEmail;


namespace Dominio.Servicos
{
    public class SendEmailService:ISendEmailService
    {
        //criar interface, aplicação e integrar no controle n hora de registrar, criar um email de teste
        private readonly IEmailSender _emailSender;

        public SendEmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task EnviarEmailAsync(EmailMessage message)
        {
            await _emailSender.SendAsync(message);
        }
    }
}



