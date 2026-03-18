using Entidades.Notificacoes;

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
