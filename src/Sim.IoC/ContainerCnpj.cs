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
    public static class ContainerCnpj
    {
        public static void DataBaseConfigCNPJ(this IServiceCollection services, IConfiguration config, string connection)
        {
            services.AddDbContext<ApplicationContextCnpj>(options => options.UseSqlServer(config.GetConnectionString(connection)));            
        }
        public static void RegisterServicesCNPJ(this IServiceCollection services)
        {
            services.AddScoped<IAppServiceBase<BaseReceitaFederal>, AppServiceBase<BaseReceitaFederal>>();
            services.AddScoped<IAppServiceCnpj, AppServiceCnpj>();

            services.AddScoped<IServiceBase<BaseReceitaFederal>, ServiceBase<BaseReceitaFederal>>();
            services.AddScoped<IServiceCnpj, ServiceCnpj>();

            services.AddScoped<IRepositoryBase<BaseReceitaFederal>, RepositoryBase<BaseReceitaFederal>>();
            services.AddScoped<IRepositoryCnpj, RepositoryCnpj>();

            services.AddScoped<ApplicationContextCnpj>();
        }
    }
}
