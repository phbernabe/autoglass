using DesafioAutoglass.Data.Mapping;
using DesafioAutoglass.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioAutoglass.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new FornecedorMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
    }
}