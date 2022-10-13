using System.Linq.Expressions;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{
    public class ServiceEmpregos : ServiceBase<Empregos>, IServiceEmpregos
    {
        private readonly IRepositoryEmpregos _repositoryEmpregos;
        public ServiceEmpregos(IRepositoryEmpregos repositoryBase) : base(repositoryBase)
        {
            _repositoryEmpregos = repositoryBase;
        }

        public async Task<IEnumerable<Empregos>> DoListAsync(Expression<Func<Empregos, bool>>? filter = null)
        {
            return await _repositoryEmpregos.DoListAsync(filter);
        }

        public async Task<IEnumerable<Empregos>> DoListEmpregosAsync()
        {
            return await DoListAsync();
        }

        public async Task<IEnumerable<Empregos>> DoListEmpregosAsyncByAno(int ano)
        {
            return await DoListAsync(s => s.Data.Value.Year == ano);
        }

        public async Task<Empregos> GetEmpregoByIdAsync(Guid id)
        {
            return await _repositoryEmpregos.GetEmpregoByIdAsync(id);
        }
    }
}
