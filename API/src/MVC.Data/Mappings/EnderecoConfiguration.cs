using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC.Business.Models;

namespace MVC.Data.Mappings
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.Logradouro)
                .IsRequired()
                .HasColumnType("VARCHAR(200)");

            builder
                .Property(p => p.Numero)
                .IsRequired()
                .HasColumnType("VARCHAR(50)");

            builder
                .Property(p => p.Cep)
                .IsRequired()
                .HasColumnType("VARCHAR(8)");

            builder
                .Property(p => p.Complemento)
                .HasColumnType("VARCHAR(250)");

            builder
                .Property(p => p.Bairro)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder
                .Property(p => p.Cidade)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder
                .Property(p => p.Estado)
                .IsRequired()
                .HasColumnType("VARCHAR(50)");

            builder.ToTable("Enderecos");
        }
    }
}