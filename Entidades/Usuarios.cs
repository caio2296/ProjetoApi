using Entidades.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuarios:Notifica
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string TokenJWT { get; set; }
        public string UsuarioTipo { get; set; } = "comum"; // Default 'comum'
    }
}
