namespace Sim.Domain.Organizacao.Interfaces.Service
{
    using Model;
    public interface IServiceServico : IServiceBase<EServico>
    {
        Task<IEnumerable<EServico>> ListServicoOwnerAsync(string setor);
        Task<EServico> GetIdAsync(Guid id);
        Task<IEnumerable<EServico>> ListAllAsync();        
        Task<IEnumerable<EServico>> DoListByDominioAsync(Guid id);
        Task<IEnumerable<(string servico, string value)>> ToListJson(string setor);
        
    }
}
