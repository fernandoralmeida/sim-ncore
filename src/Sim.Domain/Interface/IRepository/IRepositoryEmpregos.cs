namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositoryEmpregos : IRepositoryBase<Empregos>
    {
        Task<Empregos> GetEmpregoByIdAsync(Guid id);
        Task<IEnumerable<Empregos>> DoListEmpregosAsync();
        Task<IEnumerable<Empregos>> DoListEmpregosAsyncBy(string param);
        Task<IEnumerable<Empregos>> DoListEmpregosAsyncByAno(int ano);
    }
}
