using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Cnpj.Entity;

namespace Sim.Data.Cnpj.Config.Entity
{
    public class CnaePrincipalMap : IEntityTypeConfiguration<CNAEPrincipal>
    {
        public void Configure(EntityTypeBuilder<CNAEPrincipal> builder)
        {
            builder.HasNoKey();
            builder.Property(c => c.Codigo)
                .HasColumnType("varchar(10)");
            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(max)");
        }

    }
}
