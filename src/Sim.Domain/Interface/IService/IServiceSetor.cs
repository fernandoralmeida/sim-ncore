namespace Sim.Domain.Interface.IService
{
    using Entity;
    public interface IServiceSetor : IServiceBase<Setor>
    {
        Task<IEnumerable<Setor>> ListSetorOwnerAsync(string secretaria);
        Task<Setor> GetIdAsync(Guid id);
        Task<IEnumerable<Setor>> ListAllAsync();
    }
}
