using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Configuracao
{
    public class Contexto : DbContext
    {


        public Contexto (DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Frutas> Frutas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                optionsBuilder.UseSqlServer(ObterStringConexao());
            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Frutas>();
        }

        private string ObterStringConexao()
        {
            string strcon =
                "Server=REUNIAOJANUSRJ;Database=projeto;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;\r\n";
            return strcon;
        }

    }
}
