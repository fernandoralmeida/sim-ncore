using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;
using System.Linq.Expressions;

namespace Sim.Application.Services
{
    public class AppServiceStatusAtendimento: AppServiceBase<StatusAtendimento>, IAppServiceStatusAtendimento
    {
        private readonly IServiceStatusAtendimento _statusatendimento;

        public AppServiceStatusAtendimento(IServiceStatusAtendimento statusatendimento)
            : base(statusatendimento)
        {
            _statusatendimento = statusatendimento;
        }

        public async Task<StatusAtendimento> GetIdAsync(Guid id)
        {
            return await _statusatendimento.GetIdAsync(id);
        }

        public async Task<IEnumerable<StatusAtendimento>> ListAllAsync()
        {
            return await _statusatendimento.ListAllAsync();
        }

        public async Task<IEnumerable<StatusAtendimento>> ListUserAsync(string username)
        {
            return await _statusatendimento.ListUserAsync(username);
        }

        public async Task<StatusAtendimento> MyStatusAsync(string username)
        {
            return await _statusatendimento.MyStatusAsync(username);
        }
    }
}
