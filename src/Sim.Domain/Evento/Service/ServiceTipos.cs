namespace Sim.Domain.Evento.Service
{
    using Model;
    using Interfaces.Repository;
    using Interfaces.Service;
    public class ServiceTipos : ServiceBase<ETipo>, IServiceTipo
    {
        private readonly IRepositoryTipo _repositoryTipo;

        public ServiceTipos(IRepositoryTipo repositoryTipo)
            :base(repositoryTipo)
        {
            _repositoryTipo = repositoryTipo;
        }

        public async Task<ETipo> GetIdAsync(Guid id)
        {
            return await _repositoryTipo.GetIdAsync(id);
        }

        public int LastCodigo()
        {
            return 0;
        }

        public async Task<IEnumerable<ETipo>> ListAllAsync()
        {
            return await _repositoryTipo.ListAllAsync();
        }

        public async Task<IEnumerable<ETipo>> ListTipoOwnerAsync(string owner)
        {
            return await _repositoryTipo.ListTipoOwnerAsync(owner);
        }
    }
}
