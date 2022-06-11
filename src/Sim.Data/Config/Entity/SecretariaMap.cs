using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Entity;

namespace Sim.Data.Config.Entity
{
    public class SecretariaMap : IEntityTypeConfiguration<Secretaria>
    {
        public void Configure(EntityTypeBuilder<Secretaria> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Owner)
                .HasColumnType("varchar(max)");
        }
    }
}
