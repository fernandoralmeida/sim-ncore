using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Entity;

namespace Sim.Data.Config.Entity
{
    public class EventoMap : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Tipo)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Nome)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Owner)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Formato)
                .HasColumnType("varchar(255)");

        }
    }
}
