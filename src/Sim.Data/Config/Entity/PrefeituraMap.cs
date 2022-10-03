using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Entity;

namespace Sim.Data.Config.Entity
{
    public class PrefeituraMap : IEntityTypeConfiguration<EPrefeitura>
    {
        public void Configure(EntityTypeBuilder<EPrefeitura> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Cidade)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.UF)
                .HasColumnType("varchar(2)");
        }
    }
}