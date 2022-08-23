namespace Sim.Domain.Interface.IService
{
    using Entity;
    public interface IServiceEmpregos : IServiceBase<Empregos>
    {   
        Task<Empregos> GetEmpregoByIdAsync(Guid id);
        Task<IEnumerable<Empregos>> DoListEmpregosAsync();
        Task<IEnumerable<Empregos>> DoListEmpregosAsyncBy(string param);
        Task<IEnumerable<Empregos>> DoListEmpregosAsyncByAno(int ano);
    }
}
