using Microsoft.EntityFrameworkCore;
using YourNamespace.Models; 

namespace YourNamespace.Data 
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
      
        public DbSet<Usuario> Cadastro { get; set; }

        public DbSet<Produto> Produtos { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          
            modelBuilder.Entity<Usuario>().ToTable("Cadastro");          
            modelBuilder.Entity<Produto>().ToTable("Produto");
        }
    }
}
