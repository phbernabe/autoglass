using DesafioAutoglass.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioAutoglass.Data.Mapping
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.Property(c => c.Id)
                .UseIdentityColumn();

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(c => c.Situacao)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(c => c.DataFabricacao)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(c => c.DataValidade)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.HasQueryFilter(x => x.Situacao != Situacao.Inativo);
        }
    }
}
