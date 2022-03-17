using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC.Business.Models;

namespace MVC.Data.Mappings
{
    public class FornecedorConfiguration : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("VARCHAR(200)");

            builder
                .Property(p => p.Documento)
                .IsRequired()
                .HasColumnType("VARCHAR(14)");

            // 1 : 1 => Fornecedor : Endereco
            builder
                .HasOne(p => p.Endereco)
                .WithOne(p => p.Fornecedor);

            // 1 : N => Fornecedor : Produtos
            builder
                .HasMany(p => p.Produtos)
                .WithOne(p => p.Fornecedor)
                .HasForeignKey(p => p.FornecedorId);

            builder.ToTable("Fornecedores");
        }
    }
}