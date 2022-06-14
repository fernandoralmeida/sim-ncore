using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceEmpregos : IAppServiceBase<Empregos>
    {
        Task<IEnumerable<Empregos>> ListEmpregosAsync();
        Task<IEnumerable<Empregos>> ListEmpregosAsync(string cnpj);
        Task<Empregos> GetIdAsync(Guid id);
        Task<IEnumerable<Empregos>> ListAllAsync();
    }
}
