namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositoryEmpregos : IRepositoryBase<Empregos>
    {
        Task<IEnumerable<Empregos>> ListEmpregosAsync();
        Task<IEnumerable<Empregos>> ListEmpregosAsync(string cnpj);
        Task<Empregos> GetIdAsync(Guid id);
        Task<IEnumerable<Empregos>> ListAllAsync();
    }
}
