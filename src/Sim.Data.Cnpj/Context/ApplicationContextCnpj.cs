using Microsoft.EntityFrameworkCore;
using Sim.Domain.Cnpj.Entity;
using Sim.Data.Cnpj.Config.Entity;

namespace Sim.Data.Cnpj.Context
{  

    public class ApplicationContextCnpj : DbContext
    {

        public ApplicationContextCnpj()
        { }

        public ApplicationContextCnpj(DbContextOptions<ApplicationContextCnpj> options) : base(options)
        {  }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("Sim_Data_RFB"));
            }
        }
        */
        public DbSet<CNAE> Cnaes { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Estabelecimento> Estabelecimentos { get; set; }
        public DbSet<MotivoSituacaoCadastral> MotivoSituacaoCadastral { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<NaturezaJuridica> NaturezaJuridica { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<QualificacaoSocio> QualificacaoSocios { get; set; }
        public DbSet<Simples> Simples { get; set; }
        public DbSet<Socio> Socios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CnaeMap());
            modelBuilder.ApplyConfiguration(new EmpresaMap());
            modelBuilder.ApplyConfiguration(new EstabelecimentoMap());
            modelBuilder.ApplyConfiguration(new MotivoSituacaoCadastralMap());
            modelBuilder.ApplyConfiguration(new MunicipioMap());
            modelBuilder.ApplyConfiguration(new NaturezaJuridicaMap());
            modelBuilder.ApplyConfiguration(new PaisMap());
            modelBuilder.ApplyConfiguration(new QualificacaoSocioMap());
            modelBuilder.ApplyConfiguration(new SimplesMap());
            modelBuilder.ApplyConfiguration(new SociosMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
