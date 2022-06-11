namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositorySetor : IRepositoryBase<Setor>
    {
        Task<IEnumerable<Setor>> ListSetorOwnerAsync(string secretaria);
        Task<Setor> GetIdAsync(Guid id);
        Task<IEnumerable<Setor>> ListAllAsync();
    }
}
