using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Sim.Domain;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Service;

using Sim.Application;
using Sim.Application.Interfaces;
using Sim.Application.Services;

using Sim.Application.WebService.RWS.Services;

using Sim.Data;
using Sim.Data.Context;
using Sim.Data.Repository;

namespace Sim.IoC
{
    public class Container
    {
        public static void RegisterDataContext(IServiceCollection services, IConfiguration config, string connection)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(config.GetConnectionString(connection)));
            services.AddScoped<DbContext, ApplicationContext>();
            services.AddScoped<IReceitaWS, ReceitaWS>();
        }
        public static void ApplicationContextServices(IServiceCollection services)
        {
            AddPessoaServices(services);
            AddEmpresaServices(services);
            AddAtendimentoServices(services);
            AddSecretariaServices(services);
            AddSetorServices(services);
            AddServicoServices(services);
            AddEventoServices(services);
            AddCanalServices(services);
            AddContadorServices(services);
            AddTiposEventosServices(services);
            AddTiposInscricaoEventosServices(services);
            AddPlannerServices(services);
            AddParceiroServices(services);
            AddEmpregosServices(services);            
        }

        private static void AddPessoaServices(IServiceCollection services)
        {
            services.AddScoped<IAppServiceBase<Pessoa>, AppServiceBase<Pessoa>>();
            services.AddScoped<IAppServicePessoa, AppServicePessoa>();

            services.AddScoped<IServiceBase<Pessoa>, ServiceBase<Pessoa>>();
            services.AddScoped<IServicePessoa, ServicePessoa>();

            services.AddScoped<IRepositoryBase<Pessoa>, RepositoryBase<Pessoa>>();
            services.AddScoped<IRepositoryPessoa, RepositoryPessoa>();
        }

        private static void AddEmpresaServices(IServiceCollection services)
        {
            /**/
            services.AddScoped<IAppServiceBase<Empresas>, AppServiceBase<Empresas>>();
            services.AddScoped<IAppServiceEmpresa, AppServiceEmpresa>();

            services.AddScoped<IServiceBase<Empresas>, ServiceBase<Empresas>>();
            services.AddScoped<IServiceEmpresa, ServiceEmpresa>();

            services.AddScoped<IRepositoryBase<Empresas>, RepositoryBase<Empresas>>();
            services.AddScoped<IRepositoryEmpresa, RepositoryEmpresa>();

        }

        private static void AddAtendimentoServices(IServiceCollection services)
        {
            //
            services.AddScoped<IAppServiceBase<Atendimento>, AppServiceBase<Atendimento>>();
            services.AddScoped<IAppServiceAtendimento, AppServiceAtendimento>();

            services.AddScoped<IServiceBase<Atendimento>, ServiceBase<Atendimento>>();
            services.AddScoped<IServiceAtendimento, ServiceAtendimento>();

            services.AddScoped<IRepositoryBase<Atendimento>, RepositoryBase<Atendimento>>();
            services.AddScoped<IRepositoryAtendimento, RepositoryAtendimento>();

            // Status Atendimento
            services.AddScoped<IAppServiceBase<StatusAtendimento>, AppServiceBase<StatusAtendimento>>();
            services.AddScoped<IAppServiceStatusAtendimento, AppServiceStatusAtendimento>();

            services.AddScoped<IServiceBase<StatusAtendimento>, ServiceBase<StatusAtendimento>>();
            services.AddScoped<IServiceStatusAtendimento, ServiceStatusAtendimento>();

            services.AddScoped<IRepositoryBase<StatusAtendimento>, RepositoryBase<StatusAtendimento>>();
            services.AddScoped<IRepositoryStatusAtendimento, RepositoryStatusAtendimento>();
        }

        private static void AddSecretariaServices(IServiceCollection services)
        {
            //
            services.AddScoped<IAppServiceBase<Secretaria>, AppServiceBase<Secretaria>>();
            services.AddScoped<IAppServiceSecretaria, AppServiceSecretaria>();

            services.AddScoped<IServiceBase<Secretaria>, ServiceBase<Secretaria>>();
            services.AddScoped<IServiceSecretaria, ServiceSecretaria>();

            services.AddScoped<IRepositoryBase<Secretaria>, RepositoryBase<Secretaria>>();
            services.AddScoped<IRepositorySecretaria, RepositorySecretaria>();
        }

        private static void AddSetorServices(IServiceCollection services)
        {
            //
            services.AddScoped<IAppServiceBase<Setor>, AppServiceBase<Setor>>();
            services.AddScoped<IAppServiceSetor, AppServiceSetor>();

            services.AddScoped<IServiceBase<Setor>, ServiceBase<Setor>>();
            services.AddScoped<IServiceSetor, ServiceSetor>();

            services.AddScoped<IRepositoryBase<Setor>, RepositoryBase<Setor>>();
            services.AddScoped<IRepositorySetor, RepositorySetor>();
        }

        private static void AddServicoServices(IServiceCollection services)
        {
            //
            services.AddScoped<IAppServiceBase<Servico>, AppServiceBase<Servico>>();
            services.AddScoped<IAppServiceServico, AppServiceServico>();

            services.AddScoped<IServiceBase<Servico>, ServiceBase<Servico>>();
            services.AddScoped<IServiceServico, ServiceServico>();

            services.AddScoped<IRepositoryBase<Servico>, RepositoryBase<Servico>>();
            services.AddScoped<IRepositoryServico, RepositoryServico>();
        }

        private static void AddEventoServices(IServiceCollection services)
        {
            //
            services.AddScoped<IAppServiceBase<Evento>, AppServiceBase<Evento>>();
            services.AddScoped<IAppServiceEvento, AppServiceEvento>();

            services.AddScoped<IServiceBase<Evento>, ServiceBase<Evento>>();
            services.AddScoped<IServiceEvento, ServiceEvento>();

            services.AddScoped<IRepositoryBase<Evento>, RepositoryBase<Evento>>();
            services.AddScoped<IRepositoryEvento, RepositoryEvento>();
        }

        private static void AddCanalServices(IServiceCollection services)
        {
            //
            services.AddScoped<IAppServiceBase<Canal>, AppServiceBase<Canal>>();
            services.AddScoped<IAppServiceCanal, AppServiceCanal>();

            services.AddScoped<IServiceBase<Canal>, ServiceBase<Canal>>();
            services.AddScoped<IServiceCanal, ServiceCanal>();

            services.AddScoped<IRepositoryBase<Canal>, RepositoryBase<Canal>>();
            services.AddScoped<IRepositoryCanal, RepositoryCanal>();
        }

        private static void AddContadorServices(IServiceCollection services)
        {
            services.AddScoped<IAppServiceBase<Contador>, AppServiceBase<Contador>>();
            services.AddScoped<IAppServiceContador, AppServiceContador>();

            services.AddScoped<IServiceBase<Contador>, ServiceBase<Contador>>();
            services.AddScoped<IServiceContador, ServiceContador>();

            services.AddScoped<IRepositoryBase<Contador>, RepositoryBase<Contador>>();
            services.AddScoped<IRepositoryContador, RepositoryContador>();
        }

        private static void AddTiposEventosServices(IServiceCollection services)
        {
            services.AddScoped<IAppServiceBase<Tipo>, AppServiceBase<Tipo>>();
            services.AddScoped<IAppServiceTipo, AppServiceTipo>();

            services.AddScoped<IServiceBase<Tipo>, ServiceBase<Tipo>>();
            services.AddScoped<IServiceTipo, ServiceTipos>();

            services.AddScoped<IRepositoryBase<Tipo>, RepositoryBase<Tipo>>();
            services.AddScoped<IRepositoryTipo, RepositoryTipo>();
        }

        private static void AddTiposInscricaoEventosServices(IServiceCollection services)
        {
            services.AddScoped<IAppServiceBase<Inscricao>, AppServiceBase<Inscricao>>();
            services.AddScoped<IAppServiceInscricao, AppServiceInscricao>();

            services.AddScoped<IServiceBase<Inscricao>, ServiceBase<Inscricao>>();
            services.AddScoped<IServiceInscricao, ServiceInscricao>();

            services.AddScoped<IRepositoryBase<Inscricao>, RepositoryBase<Inscricao>>();
            services.AddScoped<IRepositoryInscricao, RepositoryInscricao>();
        }

        private static void AddPlannerServices(IServiceCollection services)
        {
            services.AddScoped<IAppServiceBase<Planner>, AppServiceBase<Planner>>();
            services.AddScoped<IAppServicePlaner, AppServicePlaner>();

            services.AddScoped<IServiceBase<Planner>, ServiceBase<Planner>>();
            services.AddScoped<IServicePlaner, ServicePlaner>();

            services.AddScoped<IRepositoryBase<Planner>, RepositoryBase<Planner>>();
            services.AddScoped<IRepositoryPlaner, RepositoryPlaner>();
        }

        private static void AddParceiroServices(IServiceCollection services)
        {
            services.AddScoped<IAppServiceBase<Parceiro>, AppServiceBase<Parceiro>>();
            services.AddScoped<IAppServiceParceiro, AppServiceParceiro>();

            services.AddScoped<IServiceBase<Parceiro>, ServiceBase<Parceiro>>();
            services.AddScoped<IServiceParceiro, ServiceParceiros>();

            services.AddScoped<IRepositoryBase<Parceiro>, RepositoryBase<Parceiro>>();
            services.AddScoped<IRepositoryParceiro, RepositoryParceiro>();
        }

        private static void AddEmpregosServices(IServiceCollection services)
        {
            services.AddScoped<IAppServiceBase<Empregos>, AppServiceBase<Empregos>>();
            services.AddScoped<IAppServiceEmpregos, AppServiceEmpregos>();

            services.AddScoped<IServiceBase<Empregos>, ServiceBase<Empregos>>();
            services.AddScoped<IServiceEmpregos, ServiceEmpregos>();

            services.AddScoped<IRepositoryBase<Empregos>, RepositoryBase<Empregos>>();
            services.AddScoped<IRepositoryEmpregos, RepositoryEmpregos>();
        }
    }
}
