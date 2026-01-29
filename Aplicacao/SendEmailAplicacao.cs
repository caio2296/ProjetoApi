using Aplicacao.Interface;
using Dominio.Interface;
using Entidades;
using Entidades.SendEmail;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao
{
    public class SendEmailAplicacao: ISendEmailAplicacao
    {
        private ISendEmailService _sendEmailService;

        public SendEmailAplicacao(ISendEmailService sendEmailService)
        {
            _sendEmailService = sendEmailService;
        }

        public async Task EnviarEmailAsync(Usuarios registro)
        {
            try
            {
                EmailMessage mensagem = new EmailMessage()
                {
                    To = new List<string>() { registro.Email },
                    Subject = "Registro Bem Sucedido!",
                    Body = "Seja bem vindo ao nosso sistema!",
                    Attachments = null

                };
                await _sendEmailService.EnviarEmailAsync(mensagem);
            }
            catch (SmtpException)
            {

                throw;
            }
           

        }
    }
}
