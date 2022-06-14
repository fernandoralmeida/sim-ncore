using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Cnpj.Entity;

namespace Sim.Data.Cnpj.Config.Entity
{    
    public class BaseReceitaFederalMap : IEntityTypeConfiguration<BaseReceitaFederal>
    {
        public void Configure(EntityTypeBuilder<BaseReceitaFederal> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.CNPJ)
                .HasColumnType("varchar(256)");
        }
    }
}
