using Microsoft.EntityFrameworkCore;
using MVC.Business.Models;

namespace MVC.Data.Context
{
    public class MeuDbContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        public MeuDbContext(DbContextOptions<MeuDbContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var properties = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(p => p.GetProperties())
                .Where(p => p.ClrType == typeof(string)
                    && p.GetColumnType() is null);

            foreach (var property in properties)
                property.SetIsUnicode(false);

            modelBuilder.ApplyConfigurationsFromAssembly(assembly: GetType().Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}