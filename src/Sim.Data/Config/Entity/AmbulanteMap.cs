using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Entity;

namespace Sim.Data.Config.Entity
{    
    public class AmbulanteMap : IEntityTypeConfiguration<Ambulante>
    {
        public void Configure(EntityTypeBuilder<Ambulante> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Protocolo)
                .HasColumnType("varchar(256)");
            builder.HasIndex(c => c.Protocolo).IsUnique();
            builder.Property(c => c.Protocolo)
                .IsRequired();

            builder.Property(c => c.FormaAtuacao)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Local)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Atividade)
                .HasColumnType("varchar(max)");

        }
    }
}
