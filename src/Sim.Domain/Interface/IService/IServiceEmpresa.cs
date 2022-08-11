namespace Sim.Domain.Interface.IService
{
    using Entity;

    public interface IServiceEmpresa : IServiceBase<Empresas>
    {
        Task<IEnumerable<Empresas>> DoListAsyncBy(string param);
        Task<IEnumerable<Empresas>> ConsultaCNPJAsync(string cnpj);
        Task<IEnumerable<Empresas>> ConsultaRazaoSocialAsync(string name);
        Task<IEnumerable<Empresas>> ListTop20Async();

        /** Consultas Especificas **/
        Task<IEnumerable<Empresas>> ListEmpresasAsync(List<object> lparam);
        Task<Empresas> GetIdAsync(Guid id);
        Task<IEnumerable<Empresas>> ListAllAsync();
    }
}
