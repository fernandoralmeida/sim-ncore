using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceEmpresa : IAppServiceBase<Empresas>
    {
        Task<IEnumerable<Empresas>> DoListAsyncBy(string param);
        Task<IEnumerable<Empresas>> ConsultaCNPJAsync(string cnpj);
        Task<IEnumerable<Empresas>> ConsultaRazaoSocialAsync(string name);
        Task<IEnumerable<Empresas>> ListTop20Async();

        /** Consultas Especificas **/
        Task<IEnumerable<Empresas>> ListEmpresasAsync(List<object> lparam);
        Task<Empresas> GetIdAsync(Guid id);
        Task<IEnumerable<Empresas>> ListAllAsync();
        Task<IEnumerable<Empresas>> DoListOnlyUnlinkeds();
    }
}
