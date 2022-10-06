namespace Sim.Domain.Evento.Service
{
    using Model;
    using Evento.Interfaces.Repository;
    using Evento.Interfaces.Service;
    public class ServiceParceiros : ServiceBase<EParceiro>, IServiceParceiro
    {
        private readonly IRepositoryParceiro _repositoryParceiro;

        public ServiceParceiros(IRepositoryParceiro repositoryParceiro)
            : base(repositoryParceiro)
        {
            _repositoryParceiro = repositoryParceiro;
        }

        public async Task<EParceiro> GetIdAsync(Guid id)
        {
            return await _repositoryParceiro.GetIdAsync(id);
        }

        public async Task<IEnumerable<EParceiro>> ListAllAsync()
        {
            return await _repositoryParceiro.ListAllAsync();
        }

        public async Task<IEnumerable<EParceiro>> ListParceirosAsync(string owner)
        {
            return await _repositoryParceiro.ListParceirosAsync(owner);
        }
    }
}
