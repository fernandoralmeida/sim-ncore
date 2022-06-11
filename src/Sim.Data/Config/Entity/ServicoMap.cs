using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Entity;

namespace Sim.Data.Config.Entity
{
    public class ServicoMap : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome)
                .HasColumnType("varchar(max)");

        }
    }
}
