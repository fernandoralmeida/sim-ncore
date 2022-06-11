using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Entity;

namespace Sim.Data.Config.Entity
{
    public class ParceiroMap : IEntityTypeConfiguration<Parceiro>
    {
        public void Configure(EntityTypeBuilder<Parceiro> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome)
                .HasColumnType("varchar(max)");
        }
    }
}
