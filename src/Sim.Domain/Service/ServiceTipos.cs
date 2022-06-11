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

        public int LastCodigo()
        {
            return 0;
        }
        public async Task<IEnumerable<Tipo>> ListTipoOwnerAsync(string owner)
        {
            return await _repositoryTipo.ListTipoOwnerAsync(owner);
        }
    }
}
