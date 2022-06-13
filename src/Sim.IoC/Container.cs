using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sim.Identity.Entity;
using Sim.Data.Context;
using Microsoft.Extensions.Configuration;

namespace Sim.IoC
{
    public class Container
    {

        //registra o dbcontext aos serviços
        public static void RegisterDataContext(IServiceCollection services, IConfiguration config, string connection)
        {
            _ = new ApplicationContext(config.GetConnectionString(connection));

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(config.GetConnectionString(connection)));

            services.AddScoped<DbContext, ApplicationContext>();
        }
        public static void RegisterAppServices(IServiceCollection services)
        {
            //services.AddScoped();
        }
    }
}
