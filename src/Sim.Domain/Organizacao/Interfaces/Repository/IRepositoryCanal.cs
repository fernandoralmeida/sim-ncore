namespace Sim.Domain.Organizacao.Interfaces.Repository
{
    using Model;
    public interface IRepositoryCanal : IRepositoryBase<ECanal>
    {
        Task<IEnumerable<ECanal>> ListCanalOwner(string setor);
        Task<ECanal> GetIdAsync(Guid id);
        Task<IEnumerable<ECanal>> ListAllAsync();
    }
}
