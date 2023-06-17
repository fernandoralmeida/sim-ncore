using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{
    public class ServicePessoa : ServiceBase<Pessoa>, IServicePessoa
    {
        private readonly IRepositoryPessoa _repositoryPessoa;

        public ServicePessoa(IRepositoryPessoa repositoryPessoa):base(repositoryPessoa)
        {
            _repositoryPessoa = repositoryPessoa;
        }

        public async Task<IEnumerable<Pessoa>> ConsultaCPFAsync(string cpf)
        {
            return await _repositoryPessoa.ConsultaCPFAsync(cpf);
        }

        public async Task<IEnumerable<Pessoa>> ConsultaNomeAsync(string nome)
        {
            return await _repositoryPessoa.ConsultaNomeAsync(nome);
        }

        public async Task<IEnumerable<Pessoa>> DoListAsyncBy(string param)
        {
            return await _repositoryPessoa.DoListAsyncBy(param);
        }

        public async Task<IEnumerable<Pessoa>> DoListOnlyUnlinkeds()
            => await _repositoryPessoa.DoListOnlyUnlinkeds();

        public async Task<Pessoa> GetIdAsync(Guid id)
        {
            return await _repositoryPessoa.GetIdAsync(id);
        }

        public async Task<IEnumerable<Pessoa>> ListAllAsync()
        {
            return await _repositoryPessoa.ListAllAsync();
        }

        public async Task<IEnumerable<Pessoa>> ListTop10Async()
        {
            return await _repositoryPessoa.ListTop10Async();
        }

    }
}
