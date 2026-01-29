using Dominio.Interface;
using Entidades.SendEmail;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infraestrutura.SendEmail
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly EmailSettings _settings;

        public SmtpEmailSender(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendAsync(EmailMessage message)
        {
            var mailMessage = ConvertToMailMessage(message);

            using var smtpClient = new SmtpClient(_settings.Provedor)
            {
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                    _settings.Username,
                    _settings.Password
                )
            };

           await smtpClient.SendMailAsync(mailMessage);
        }

        private MailMessage ConvertToMailMessage(EmailMessage message)
        {
            var mail = new MailMessage
            {
                From = new MailAddress(_settings.Username),
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true
            };

            foreach (var to in message.To) 
            {
                ValidarEmailOuLancarExcecao(to);

                  mail.To.Add(to);
            }

            if (message.Attachments != null)
            {
                foreach (var file in message.Attachments)
                    mail.Attachments.Add(new Attachment(file));
            }

            return mail;
        }

        private bool ValidateEmail(string email)
        {
            Regex expression = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
            if (expression.IsMatch(email))
                return true;

            return false;
        }
        private  bool DominioPossuiMX(string email)
        {
            var dominio = email.Split('@')[1];
            var host = System.Net.Dns.GetHostEntry(dominio);
           
               if(host.AddressList.Length > 0)
                   return true;
            
           return false;
            
        }

        private void ValidarEmailOuLancarExcecao(string email)
        {
            if (!ValidateEmail(email))
                throw new ArgumentException($"Email inválido: {email}");

            if (!DominioPossuiMX(email))
                throw new InvalidOperationException($"Domínio sem MX: {email}");
        }

    }
}
