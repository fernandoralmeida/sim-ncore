using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{
    public class AppServiceEmpregos : AppServiceBase<Empregos>, IAppServiceEmpregos
    {
        private readonly IServiceEmpregos _db;
        public AppServiceEmpregos(IServiceEmpregos serviceEmpregos) : base(serviceEmpregos) { _db = serviceEmpregos; }

        public async Task<IEnumerable<Empregos>> DoListEmpregosAsync()
        {
            return await _db.DoListEmpregosAsync();
        }

        public async Task<IEnumerable<Empregos>> DoListEmpregosAsyncBy(string param)
        {
            return await _db.DoListEmpregosAsyncBy(param);
        }

        public async Task<IEnumerable<Empregos>> DoListEmpregosAsyncByAno(int ano)
        {
           return await _db.DoListEmpregosAsyncByAno(ano);
        }

        public async Task<Empregos> GetEmpregoByIdAsync(Guid id)
        {
            return await _db.GetEmpregoByIdAsync(id);
        }
    }
}
