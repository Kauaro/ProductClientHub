using Microsoft.EntityFrameworkCore;
using ProductClientHub.API.Entities;

namespace ProductClientHub.API.Infrastructure
{
    public class ProductClientHubDbContext : DbContext
    {
        public ProductClientHubDbContext(DbContextOptions<ProductClientHubDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; } = default!;
        public DbSet<Projeto> Projeto { get; set; } = default!;

        // Opcional: se quiser ainda usar OnConfiguring como fallback
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         optionsBuilder.UseSqlite("Data Source=C:\\Projetos\\C#\\API_SLA\\BDSLA.db");
        //     }
        // }
    }
}


// RESOLVER O ERRO DO "ProductClientHubDbContext" !!!!!!!!!!!!!!!!!!!!!!!URGENTE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!