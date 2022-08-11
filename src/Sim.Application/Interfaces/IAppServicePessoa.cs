using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServicePessoa : IAppServiceBase<Pessoa>
    {
        Task<IEnumerable<Pessoa>> DoListAsyncBy(string param);
        Task<IEnumerable<Pessoa>> ConsultaNomeAsync(string nome);
        Task<IEnumerable<Pessoa>> ConsultaCPFAsync(string cpf);
        Task<IEnumerable<Pessoa>> ListTop10Async();
        Task<Pessoa> GetIdAsync(Guid id);
        Task<IEnumerable<Pessoa>> ListAllAsync();
    }
}
