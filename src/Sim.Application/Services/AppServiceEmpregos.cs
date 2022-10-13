using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;
using System.Linq.Expressions;

namespace Sim.Application.Services
{
    public class AppServiceEmpregos : AppServiceBase<Empregos>, IAppServiceEmpregos
    {
        private readonly IServiceEmpregos _db;
        public AppServiceEmpregos(IServiceEmpregos serviceEmpregos) : base(serviceEmpregos) { _db = serviceEmpregos; }

        public async Task<IEnumerable<Empregos>> DoListAsync(Expression<Func<Empregos, bool>> filter = null)
        {
            return await _db.DoListAsync(filter);
        }
        public async Task<Empregos> GetEmpregoByIdAsync(Guid id)
        {
            return await _db.GetEmpregoByIdAsync(id);
        }
    }
}
