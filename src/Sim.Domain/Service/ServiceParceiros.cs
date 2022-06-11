using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{

    public class ServiceParceiros : ServiceBase<Parceiro>, IServiceParceiro
    {
        private readonly IRepositoryParceiro _repositoryParceiro;

        public ServiceParceiros(IRepositoryParceiro repositoryParceiro)
            : base(repositoryParceiro)
        {
            _repositoryParceiro = repositoryParceiro;
        }

        public async Task<IEnumerable<Parceiro>> ListParceirosAsync(string owner)
        {
            return await _repositoryParceiro.ListParceirosAsync(owner);
        }
    }
}
