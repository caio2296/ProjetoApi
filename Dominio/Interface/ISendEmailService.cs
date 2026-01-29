using Entidades.SendEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface
{
    public interface ISendEmailService
    {
        Task EnviarEmailAsync(EmailMessage message);
    }
}
