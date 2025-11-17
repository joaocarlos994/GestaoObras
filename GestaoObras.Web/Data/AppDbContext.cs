using GestaoObras.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoObras.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }     

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Material> Materiais { get; set; }
        public DbSet<Obra> Obras { get; set; }
        public DbSet<MovimentoStock> MovimentosStock { get; set; }
        public DbSet<RegistoMaoObra> RegistosMaoObra { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=GestaoObrasDb;Username=postgres;Password=1234;");
        }
    }
}