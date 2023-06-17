using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{
   
    public class AppServiceEmpresa : AppServiceBase<Empresas>, IAppServiceEmpresa
    {
        private readonly IServiceEmpresa _empresa;

        public AppServiceEmpresa(IServiceEmpresa empresa)
            :base(empresa)
        {
            _empresa = empresa;
        }

        public async Task<IEnumerable<Empresas>> ConsultaCNPJAsync(string cnpj)
        {
            return await _empresa.ConsultaCNPJAsync(cnpj);
        }

        public async Task<IEnumerable<Empresas>> ConsultaRazaoSocialAsync(string name)
        {
            return await _empresa.ConsultaRazaoSocialAsync(name);
        }

        public async Task<IEnumerable<Empresas>> DoListAsyncBy(string param)
        {
            return await _empresa.DoListAsyncBy(param);
        }

        public async Task<IEnumerable<Empresas>> DoListOnlyUnlinkeds()
            => await _empresa.DoListOnlyUnlinkeds();

        public async Task<Empresas> GetIdAsync(Guid id)
        {
            return await _empresa.GetIdAsync(id);
        }

        public async Task<IEnumerable<Empresas>> ListAllAsync()
        {
            return await _empresa.ListAllAsync();
        }

        public async Task<IEnumerable<Empresas>> ListEmpresasAsync(List<object> lparam)
        {
            return await _empresa.ListEmpresasAsync(lparam);    
        }

        public async Task<IEnumerable<Empresas>> ListTop20Async()
        {
            return await _empresa.ListTop20Async();
        }
    }
}
