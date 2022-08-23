using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceEmpregos : IAppServiceBase<Empregos>
    {
        Task<Empregos> GetEmpregoByIdAsync(Guid id);
        Task<IEnumerable<Empregos>> DoListEmpregosAsync();
        Task<IEnumerable<Empregos>> DoListEmpregosAsyncBy(string param);
        Task<IEnumerable<Empregos>> DoListEmpregosAsyncByAno(int ano);
    }
}
