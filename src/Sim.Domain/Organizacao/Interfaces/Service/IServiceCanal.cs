namespace Sim.Domain.Organizacao.Interfaces.Service
{
    using Model;
    public interface IServiceCanal : IServiceBase<ECanal>
    {
        Task<IEnumerable<ECanal>> ListCanalOwner(string setor);
        Task<ECanal> GetIdAsync(Guid id);
        Task<IEnumerable<ECanal>> ListAllAsync();
        Task<IEnumerable<(string canal, string value)>> ToListJson(string setor);
    }
}
