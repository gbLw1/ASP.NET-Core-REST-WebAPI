using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC.Business.Models;

namespace MVC.Data.Mappings
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("VARCHAR(200)");

            builder
                .Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("VARCHAR(1000)");

            builder
                .Property(p => p.Imagem)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder
                .Property(p => p.Valor)
                .IsRequired()
                .HasColumnType("DECIMAL(15,2)")
                .HasConversion<decimal>();

            builder.ToTable("Produtos");
        }
    }
}