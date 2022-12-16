using System.Linq.Expressions;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{
    public class ServiceStatusAtendimento : ServiceBase<StatusAtendimento>, IServiceStatusAtendimento
    {
        private readonly IRepositoryStatusAtendimento _statusatendimento;
        public ServiceStatusAtendimento(IRepositoryStatusAtendimento repositoryStatusAtendimento)
            : base(repositoryStatusAtendimento) {
            _statusatendimento = repositoryStatusAtendimento;
        }

        public async Task<StatusAtendimento> GetIdAsync(Guid id) {
            return await _statusatendimento.GetIdAsync(id);
        }

        public async Task<IEnumerable<StatusAtendimento>> ListAllAsync() {
            return await _statusatendimento.ListAllAsync();
        }

        public async Task<IEnumerable<StatusAtendimento>> ListUserAsync(string username) {
            return await _statusatendimento.ListUserAsync(username);
        }

        public async Task<StatusAtendimento> MyStatusAsync(string username) {
            return await _statusatendimento.MyStatusAsync(username);
        }
    }
}
