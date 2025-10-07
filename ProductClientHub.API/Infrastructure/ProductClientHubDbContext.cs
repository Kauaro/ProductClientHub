using Microsoft.EntityFrameworkCore;
using SLAProjectHub.API.Entities;

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
        public DbSet<Aluno> Aluno { get; set; } = default!;


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