namespace Sim.Domain.Evento.Service
{
<<<<<<< HEAD:src/Sim.Domain/Evento/Service/ServiceTipos.cs
    using Model;
    using Interfaces.Repository;
    using Interfaces.Service;
=======

>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd:src/Sim.Domain/Service/ServiceTipos.cs
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
