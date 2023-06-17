using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{
    public class ServiceEmpresa : ServiceBase<Empresas>, IServiceEmpresa
    {
        private readonly IRepositoryEmpresa _repositoryEmpresa;

        public ServiceEmpresa(IRepositoryEmpresa repositoryEmpresa)
            :base(repositoryEmpresa)
        {
            _repositoryEmpresa = repositoryEmpresa;
        }

        public async Task<IEnumerable<Empresas>> ConsultaCNPJAsync(string cnpj)
        {
            return await _repositoryEmpresa.ConsultaCNPJAsync(cnpj);
        }

        public async Task<IEnumerable<Empresas>> ConsultaRazaoSocialAsync(string name)
        {
            return await _repositoryEmpresa.ConsultaRazaoSocialAsync(name);
        }

        public async Task<IEnumerable<Empresas>> DoListAsyncBy(string param)
        {
            return await _repositoryEmpresa.DoListAsyncBy(param);
        }

        public async Task<IEnumerable<Empresas>> DoListOnlyUnlinkeds()
            => await _repositoryEmpresa.DoListOnlyUnlinkeds();

        public async Task<Empresas> GetIdAsync(Guid id)
        {
            return await _repositoryEmpresa.GetIdAsync(id);
        }

        public async Task<IEnumerable<Empresas>> ListAllAsync()
        {
            return await _repositoryEmpresa.ListAllAsync();
        }

        public async Task<IEnumerable<Empresas>> ListEmpresasAsync(List<object> lparam)
        {
            return await _repositoryEmpresa.ListEmpresasAsync(lparam);
        }

        public async Task<IEnumerable<Empresas>> ListTop20Async()
        {
            return await _repositoryEmpresa.ListTop20Async();
        }
    }
}
