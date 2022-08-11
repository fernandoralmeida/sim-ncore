namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositoryPessoa : IRepositoryBase<Pessoa>
    {
        Task<IEnumerable<Pessoa>> DoListAsyncBy(string param);
        Task<IEnumerable<Pessoa>> ConsultaNomeAsync(string nome);
        Task<IEnumerable<Pessoa>> ConsultaCPFAsync(string cpf);        
        Task<IEnumerable<Pessoa>> ListTop10Async();
        Task<Pessoa> GetIdAsync(Guid id);
        Task<IEnumerable<Pessoa>> ListAllAsync();
    }
}
