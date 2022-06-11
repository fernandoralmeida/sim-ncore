using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Entity;

namespace Sim.Data.Config.Entity
{
    public class EmpregosMap: IEntityTypeConfiguration<Empregos>
    {
        public void Configure(EntityTypeBuilder<Empregos> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Ocupacao)
                .HasColumnType("varchar(max)");

            builder.Property(c => c.Pagamento)
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Status)
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Salario)
                .HasColumnType("decimal(18,2)");

        }
    }
}
