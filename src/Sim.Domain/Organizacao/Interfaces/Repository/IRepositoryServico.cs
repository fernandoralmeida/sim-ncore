namespace Sim.Domain.Organizacao.Interfaces.Repository
{
    using Model;
    public interface IRepositoryServico : IRepositoryBase<EServico>
    {
        Task<IEnumerable<EServico>> ListServicoOwnerAsync(string setor);
        Task<EServico> GetIdAsync(Guid id);
        Task<IEnumerable<EServico>> ListAllAsync();
        Task<IEnumerable<EServico>> DoListByDominioAsync(Guid id);
    }
}
