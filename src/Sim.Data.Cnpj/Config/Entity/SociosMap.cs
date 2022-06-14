using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Cnpj.Entity;

namespace Sim.Data.Cnpj.Config.Entity
{
    public class SociosMap : IEntityTypeConfiguration<Socio>
    {
        public void Configure(EntityTypeBuilder<Socio> builder)
        {
            builder.HasNoKey();
            builder.Property(c => c.CNPJBase)
                .HasColumnType("varchar(10)");
            builder.Property(c => c.IdentificadorSocio)
                .HasColumnType("varchar(2)");
            builder.Property(c => c.NomeRazaoSocio)
                .HasColumnType("varchar(255)");
            builder.Property(c => c.CnpjCpfSocio)
                .HasColumnType("varchar(50)");
            builder.Property(c => c.QualificacaoSocio)
                .HasColumnType("varchar(4)");
            builder.Property(c => c.DataEntradaSociedade)
                .HasColumnType("varchar(10)");
            builder.Property(c => c.RepresentanteLegal)
                .HasColumnType("varchar(50)");
            builder.Property(c => c.NomeRepresentante)
                .HasColumnType("varchar(255)");
            builder.Property(c => c.QualificacaoRepresentanteLegal)
                .HasColumnType("varchar(4)");
            builder.Property(c => c.FaixaEtaria)
                .HasColumnType("varchar(2)");
        }
    }
}
