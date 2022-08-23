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

        public async Task<IEnumerable<Empregos>> DoListEmpregosAsync()
        {
            return await _repositoryEmpregos.DoListEmpregosAsync();
        }

        public async Task<IEnumerable<Empregos>> DoListEmpregosAsyncBy(string param)
        {
            return await _repositoryEmpregos.DoListEmpregosAsyncBy(param);
        }

        public async Task<IEnumerable<Empregos>> DoListEmpregosAsyncByAno(int ano)
        {
            return await _repositoryEmpregos.DoListEmpregosAsyncByAno(ano);
        }

        public async Task<Empregos> GetEmpregoByIdAsync(Guid id)
        {
            return await _repositoryEmpregos.GetEmpregoByIdAsync(id);
        }
    }
}
