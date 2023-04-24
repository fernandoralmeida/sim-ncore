using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Cnpj.Entity;

namespace Sim.Data.Cnpj.Config.Entity
{
    public class NaturezaJuridicaMap : IEntityTypeConfiguration<NaturezaJuridica>
    {
        public void Configure(EntityTypeBuilder<NaturezaJuridica> builder)
        {
            builder.HasNoKey();
            builder.Property(c => c.Codigo)
                .HasColumnType("varchar(5)");
            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(MAX)");
        }
    }
}
