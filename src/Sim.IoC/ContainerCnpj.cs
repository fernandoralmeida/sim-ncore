using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Sim.Domain.Cnpj.Entity;
using Sim.Domain.Cnpj;
using Sim.Domain.Cnpj.Interfaces;
using Sim.Domain.Cnpj.Services;

using Sim.Data.Cnpj.Context;
using Sim.Data.Cnpj;
using Sim.Data.Cnpj.Repository;

using Sim.Application.Cnpj;
using Sim.Application.Cnpj.Interfaces;
using Sim.Application.Cnpj.Services;

namespace Sim.IoC
{
    public class ContainerCnpj
    {
        public static void RegisterDataContext(IServiceCollection services, IConfiguration config, string connection)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(config.GetConnectionString(connection)));
            services.AddScoped<DbContext, ApplicationContext>();
        }
        public static void ApplicationContextServices(IServiceCollection services)
        {
            services.AddScoped<IAppServiceBase<BaseReceitaFederal>, AppServiceBase<BaseReceitaFederal>>();
            services.AddScoped<IAppServiceCnpj, AppServiceCnpj>();

            services.AddScoped<IServiceBase<BaseReceitaFederal>, ServiceBase<BaseReceitaFederal>>();
            services.AddScoped<IServiceCnpj, ServiceCnpj>();

            services.AddScoped<IRepositoryBase<BaseReceitaFederal>, RepositoryBase<BaseReceitaFederal>>();
            services.AddScoped<IRepositoryCnpj, RepositoryCnpj>();
        }
    }
}
