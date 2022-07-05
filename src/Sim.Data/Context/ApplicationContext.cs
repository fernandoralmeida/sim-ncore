using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sim.Domain.Entity;

namespace Sim.Data.Context
{   

    public class ApplicationContext : DbContext
    {
        private static string _connectionstring;
        public ApplicationContext()
        { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {  }

        public DbSet<Ambulante> Ambulante { get; set; }
        public DbSet<DIA> DIA { get; set; }
        public DbSet<Empresas> Empresa { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<RaeSebrae> Sebrae { get; set; }
        public DbSet<Empregos> Emprego { get; set; }
        public DbSet<Atendimento> Atendimento { get; set; }
        public DbSet<Canal> Canal { get; set; }
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Parceiro> Parceiro { get; set; }
        public DbSet<Planner> Planner { get; set; }
        public DbSet<Secretaria> Secretaria { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<Setor> Setor { get; set; }
        public DbSet<Inscricao> Inscricao { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Contador> Contador { get; set; }
        public DbSet<StatusAtendimento> StatusAtendimento { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ambulante>().ToTable("Ambulante");
            modelBuilder.Entity<Empregos>().ToTable("Empregos");
            modelBuilder.Entity<DIA>().ToTable("DIA");
            modelBuilder.Entity<Pessoa>().ToTable("Pessoa");
            modelBuilder.Entity<Empresas>().ToTable("Empresa");
            modelBuilder.Entity<Atendimento>().ToTable("Atendimento");
            modelBuilder.Entity<Parceiro>().ToTable("Parceiros");
            modelBuilder.Entity<Canal>().ToTable("Canal");
            modelBuilder.Entity<Evento>().ToTable("Evento");
            modelBuilder.Entity<Planner>().ToTable("Planer");
            modelBuilder.Entity<Secretaria>().ToTable("Secretaria");
            modelBuilder.Entity<Setor>().ToTable("Setor");
            modelBuilder.Entity<Servico>().ToTable("Servico");
            modelBuilder.Entity<Inscricao>().ToTable("Inscricao");
            modelBuilder.Entity<Tipo>().ToTable("Tipos");
            modelBuilder.Entity<Contador>().ToTable("Protocolos");
            modelBuilder.Entity<RaeSebrae>().ToTable("RaeSebrae");
            modelBuilder.Entity<StatusAtendimento>().ToTable("StatusAtendimento");

            modelBuilder.ApplyConfiguration(new Config.Entity.AmbulanteMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.EmpregosMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.AtendimentoMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.CanalMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.DIAMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.EmpresaMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.PessoaMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.PlannerMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.ParceiroMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.SecretariaMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.ServicoMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.SetorMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.InscricaoMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.EventoMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.TipoMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.ContadorMap());
            modelBuilder.ApplyConfiguration(new Config.Entity.StatusAtendimentoMap());

            base.OnModelCreating(modelBuilder);
        }

        public static void GetConnection(string connection)
        {
            _connectionstring = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionstring);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Data_Cadastro") != null))
            {

                if (entry.State == EntityState.Added)
                    entry.Property("Data_Cadastro").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property("Data_Cadastro").IsModified = false;
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Ultima_Alteracao") != null))
            {
                entry.Property("Ultima_Alteracao").CurrentValue = DateTime.Now;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
