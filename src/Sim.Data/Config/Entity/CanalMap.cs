using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Entity;

namespace Sim.Data.Config.Entity
{
    public class CanalMap : IEntityTypeConfiguration<Canal>
    {
        public void Configure(EntityTypeBuilder<Canal> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome)
                .HasColumnType("varchar(max)");

        }
    }
}
