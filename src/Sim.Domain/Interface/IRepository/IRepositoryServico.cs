namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositoryServico : IRepositoryBase<Servico>
    {
        Task<IEnumerable<Servico>> ListServicoOwnerAsync(string setor);
        Task<Servico> GetIdAsync(Guid id);
        Task<IEnumerable<Servico>> ListAllAsync();
    }
}
