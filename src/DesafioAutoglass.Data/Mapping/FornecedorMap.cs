using DesafioAutoglass.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioAutoglass.Data.Mapping
{
    public class FornecedorMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.Property(c => c.Id)
                .UseIdentityColumn();

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(c => c.Cnpj)
                .HasColumnType("varchar(14)");

            //builder.HasQueryFilter(p => p.Situacao == Situacao.Ativo);
        }
    }
}
