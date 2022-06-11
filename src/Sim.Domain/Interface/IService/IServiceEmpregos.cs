namespace Sim.Domain.Interface.IService
{
    using Entity;
    public interface IServiceEmpregos : IServiceBase<Empregos>
    {
        Task<IEnumerable<Empregos>> ListEmpregosAsync();
        Task<IEnumerable<Empregos>> ListEmpregosAsync(string cnpj);
        Task<Empregos> GetIdAsync(Guid id);
        Task<IEnumerable<Empregos>> ListAllAsync();
    }
}
