using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.SendEmail
{
    public class EmailMessage
    {
        //Destinatários
        public List<string> To { get; set; }
        //Assunto ou Título
        public string Subject { get; set; }
        //Conteúdo
        public string Body { get; set; }
        //Arquivo
        public List<string>? Attachments { get; set; }
    }
}
