using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{
    public class ServiceStatusAtendimento : ServiceBase<StatusAtendimento>, IServiceStatusAtendimento
    {
        private readonly IRepositoryStatusAtendimento _statusatendimento;
        public ServiceStatusAtendimento(IRepositoryStatusAtendimento repositoryStatusAtendimento)
            : base(repositoryStatusAtendimento)
        {
            _statusatendimento = repositoryStatusAtendimento;
        }

        public async Task<IEnumerable<StatusAtendimento>> ListUserAsync(string username)
        {
            return await _statusatendimento.ListUserAsync(username);
        }
    }
}
