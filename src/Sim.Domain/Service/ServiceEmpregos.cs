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

        public async Task<IEnumerable<Empregos>> ListEmpregosAsync()
        {
            return await _repositoryEmpregos.ListEmpregosAsync();
        }

        public async Task<IEnumerable<Empregos>> ListEmpregosAsync(string cnpj)
        {
            return await _repositoryEmpregos.ListEmpregosAsync();
        }
    }
}
