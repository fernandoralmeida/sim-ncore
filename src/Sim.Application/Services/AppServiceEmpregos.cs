using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{
    public class AppServiceEmpregos : AppServiceBase<Empregos>, IAppServiceEmpregos
    {
        private readonly IServiceEmpregos _db;
        public AppServiceEmpregos(IServiceEmpregos serviceEmpregos) : base(serviceEmpregos) { _db = serviceEmpregos; }

        public async Task<Empregos> GetIdAsync(Guid id)
        {
            return await _db.GetIdAsync(id);
        }

        public async Task<IEnumerable<Empregos>> ListAllAsync()
        {
            return await _db.ListAllAsync();
        }

        public async Task<IEnumerable<Empregos>> ListEmpregosAsync()
        {
            return await _db.ListEmpregosAsync();
        }

        public async Task<IEnumerable<Empregos>> ListEmpregosAsync(string cnpj)
        {
            return await _db.ListEmpregosAsync(cnpj);
        }
    }
}
