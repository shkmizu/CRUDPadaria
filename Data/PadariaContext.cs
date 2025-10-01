using Microsoft.EntityFrameworkCore;
using PadariaCRUD.Models;

namespace PadariaCRUD.Data
{
    public class PadariaContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public PadariaContext(DbContextOptions<PadariaContext> options) : base(options)
        {
        }
        
        // Construtor sem opções, usado para Migrations
        public PadariaContext() { }

        // Configuração da conexão (necessária para Migrations no Console App)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Conecta a um arquivo SQLite local
                optionsBuilder.UseSqlite("Data Source=PadariaDB.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed: Dados iniciais para a tabela Categoria
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nome = "Pães" },
                new Categoria { Id = 2, Nome = "Bolos e Tortas" },
                new Categoria { Id = 3, Nome = "Bebidas" }
            );
            
            // Configuração do relacionamento 1 para N
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId);
        }
    }
}