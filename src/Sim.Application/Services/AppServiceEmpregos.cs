using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{
    public class AppServiceEmpregos : AppServiceBase<Empregos>, IAppServiceEmpregos
    {
        private readonly IServiceEmpregos _db;
        public AppServiceEmpregos(IServiceEmpregos serviceEmpregos) : base(serviceEmpregos) { _db = serviceEmpregos; }

        public Task<IEnumerable<Empregos>> DoListEmpregosAsync()
        {
            return _db.DoListEmpregosAsync();
        }

        public Task<IEnumerable<Empregos>> DoListEmpregosAsyncBy(string param)
        {
            return _db.DoListEmpregosAsyncBy(param);
        }

        public async Task<Empregos> GetEmpregoByIdAsync(Guid id)
        {
            return await _db.GetEmpregoByIdAsync(id);
        }
    }
}
