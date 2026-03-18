using Entidades;

namespace Projeto.Model
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UsuarioTipo { get; set; } = "comum"; // Default 'comum'

        // conversão implícita: Usuario -> UsuarioDto
        public static implicit operator UsuarioDto(Usuarios usuario)
        {
            if (usuario == null) return null;

            return new UsuarioDto
            {
                Id = usuario.Id,
                Email = usuario.Email,
                UsuarioTipo = usuario.UsuarioTipo
            };
        }
    }
}
