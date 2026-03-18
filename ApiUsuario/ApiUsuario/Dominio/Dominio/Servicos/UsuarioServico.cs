using Dominio.Interface;
using Dominio.Servicos.Interfaces;
using Entidades;

namespace Dominio.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private IUsuario _usuario;

        public UsuarioServico(IUsuario usuario)
        {
            _usuario = usuario;
        }
        public async Task AdicionarUsarioSemEF(Usuarios usuarios)
        {
            var validarEmail = usuarios.ValidarPropriedadeString(usuarios.Email, "Email");
            if (validarEmail)
            {
                await _usuario.AdicionarUsuario(usuarios);
            }
        }

        public async Task AtualizarUsuarioSemEF(Usuarios usuarios)
        {
            var validarEmail = usuarios.ValidarPropriedadeString(usuarios.Email, "Email");
            if (validarEmail)
            {
                await _usuario.AtualizarUsuario(usuarios);
            }
        }

        public async Task<Usuarios> BuscarPorId(int id)
        {
            if (id == null)
            {
                return null;
            }

            return await _usuario.BuscarPorId(id);
        }

        public async Task DeletarUsuario(int id)
        {
            if (id == null)
            {
                return;
            }
            await _usuario.DeletarUsuario(id);
        }

        public async Task<List<Usuarios>> ListarUsuariosSemEF(int id)
        {
            var listaUsuarios = await _usuario.ListarUsuariosAdm(id);

            if (listaUsuarios == null)
            {
                return null;
            }
            return listaUsuarios;
        }

        public async Task<bool> ExisteUsuario(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return await _usuario.ExisteUsuario(email);
            }
            else
            {
                return false;
            }

        }

        public async Task<List<Usuarios>> ListarUsuariosAdm(int id)
        {
            if(id == null|| id < 0)
            {
                return null;
            }
            return await _usuario.ListarUsuariosAdm(id);
        }
        public async Task<int> RetornarIdUsuario(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return await _usuario.RetornarIdUsuario(email);
            }

             return 0;
        }
        public async Task AtualizaToken(int idUsuario, string token)
        {
            if (idUsuario <= 0)
                throw new ArgumentException("Id do usuário inválido", nameof(idUsuario));

            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token inválido", nameof(token));

            await _usuario.AtualizarToken(idUsuario, token);
        }

        public async Task<string> RetornarTipoUsuario(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }
            return await _usuario.RetornarTipoUsuario(email);
        }

        public async Task<Usuarios> RetornarUsuarioEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }
            return await _usuario.RetornarUsuarioEmail(email);
        }
    }
}
