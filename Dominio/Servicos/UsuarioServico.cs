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
             await _usuario.AdicionarUsuarioSemEF(usuarios);
            }
        }

        public async Task AtualizarUsuarioSemEF(Usuarios usuarios)
        {
            var validarEmail = usuarios.ValidarPropriedadeString(usuarios.Email, "Email");
            if (validarEmail)
            {
                await _usuario.AtualizarUsuarioSemEF(usuarios);
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
            await _usuario.DeletarUsuario(id);
        }

        public async Task<List<Usuarios>> ListarUsuariosSemEF(int id)
        {
            var listaUsuarios = await _usuario.ListarUsuariosSemEF(id);

            if (listaUsuarios == null)
            {
                return null;
            }
            return listaUsuarios;
        }
    }
}
