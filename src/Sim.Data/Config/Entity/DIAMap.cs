using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Entity;

namespace Sim.Data.Config.Entity
{
    public class DIAMap : IEntityTypeConfiguration<DIA>
    {
        public void Configure(EntityTypeBuilder<DIA> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Autorizacao).IsUnique();
            builder.Property(c => c.Autorizacao)
                .HasColumnType("varchar(256)").IsRequired();
            /*
            builder.Property(c => c.Atividade)
                .HasColumnType("varchar(256)");
            builder.Property(c => c.FormaAtuacao)
                .HasColumnType("varchar(150)");
            */
            builder.Property(c => c.Veiculo)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Processo)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.Situacao)
                .HasColumnType("varchar(max)");
        }
    }
}
