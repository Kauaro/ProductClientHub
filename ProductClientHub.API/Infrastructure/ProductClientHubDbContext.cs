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
        public DbSet<Avaliacao> Avaliacao { get; set; } = default!;

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aluno>()
                .HasAlternateKey(a => a.Matricula)
                .HasName("AK_Aluno_Matricula");

            modelBuilder.Entity<Projeto>()
                .HasAlternateKey(a => a.Codigo)
                .HasName("AK_Projeto_Codigo");


            modelBuilder.Entity<Avaliacao>()
                .HasOne(a => a.Aluno)
                .WithMany(a => a.Avaliacoes)
                .HasForeignKey(a => a.AlunoMatricula)
                .HasPrincipalKey(a => a.Matricula);


            modelBuilder.Entity<Avaliacao>()
                .HasOne(a => a.Projeto)
                .WithMany(a => a.Avaliacoes)
                .HasForeignKey(a => a.ProjetoCodigo)
                .HasPrincipalKey(a => a.Codigo);






        } 


    }
}


