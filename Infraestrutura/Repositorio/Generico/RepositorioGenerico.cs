using Dominio.Interface.Generico;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using Infraestrutura.Configuracao;

namespace Infraestrutura.Repositorio.Generico
{
    public class RepositorioGenerico<T> : IGenerico<T>, IDisposable where T : class
    {
       
        private readonly DbContextOptions<Contexto> _optionBuilder;

        public RepositorioGenerico()
        {
            _optionBuilder = new DbContextOptionsBuilder<Contexto>()

            //.UseMySql("Server=localhost;DataBase=autoregistro;Uid=root;Pwd=zxcasd384!A",
            // new MySqlServerVersion(new Version(8, 0, 37)))
            .UseSqlServer("Server=REUNIAOJANUSRJ;Database=projeto;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;")
            .Options;
        }

        public async Task Adicionar(T Objeto)
        {
            try
            {
                using (var data = new Contexto(_optionBuilder))
                {
                    data.Set<T>().Add(Objeto);
                    await data.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public Task AdicionarFrutasSemEF(T Objeto)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarFrutaSemEF(T Objeto)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ListarFrutasSemEF()
        {
            throw new NotImplementedException();
        }

        public async Task<T?> BuscarPorId(string id)
        {
            throw new NotImplementedException();
        }


        public async Task Atualizar(T Objeto)
        {
            using (var data = new Contexto(_optionBuilder))
            {
                data.Set<T>().Update(Objeto);
                await data.SaveChangesAsync();
            }
        }

        //public async Task<T?> BuscarPorId(string id)
        //{
        //    using (var data = new Contexto(_optionBuilder))
        //    {
        //        return await data.Set<T>()
        //            .FindAsync(id);
        //    }
        //}


        public async Task Excluir(T Objeto)
        {
            using (var data = new Contexto(_optionBuilder))
            {
                data.Set<T>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<List<T>> Listar()
        {
            using (var data = new Contexto(_optionBuilder))
            {
                return await data.Set<T>().AsNoTracking().ToListAsync();
            }
        }
        #region Disposed https://learn.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // To detect redundant calls
        private bool _disposedValue;

        // Instantiate a SafeHandle instance.
        private SafeHandle? _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _safeHandle?.Dispose();
                    _safeHandle = null;
                }

                _disposedValue = true;
            }
        }


        #endregion
    }
}
