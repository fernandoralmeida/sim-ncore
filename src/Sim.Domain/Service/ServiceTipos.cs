using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{

    public class ServiceTipos : ServiceBase<Tipo>, IServiceTipo
    {
        private readonly IRepositoryTipo _repositoryTipo;

        public ServiceTipos(IRepositoryTipo repositoryTipo)
            :base(repositoryTipo)
        {
            _repositoryTipo = repositoryTipo;
        }

        public async Task<Tipo> GetIdAsync(Guid id)
        {
            return await _repositoryTipo.GetIdAsync(id);
        }

        public int LastCodigo()
        {
            return 0;
        }

        public async Task<IEnumerable<Tipo>> ListAllAsync()
        {
            return await _repositoryTipo.ListAllAsync();
        }

        public async Task<IEnumerable<Tipo>> ListTipoOwnerAsync(string owner)
        {
            return await _repositoryTipo.ListTipoOwnerAsync(owner);
        }
    }
}
