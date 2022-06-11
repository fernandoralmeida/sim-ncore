namespace Sim.Domain.Interface.IService
{
    using Entity;
    public interface IServiceServico : IServiceBase<Servico>
    {
        Task<IEnumerable<Servico>> ListServicoOwnerAsync(string setor);
        Task<Servico> GetIdAsync(Guid id);
        Task<IEnumerable<Servico>> ListAllAsync();
    }
}
