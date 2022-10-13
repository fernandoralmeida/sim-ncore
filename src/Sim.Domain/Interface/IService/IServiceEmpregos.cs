using System.Linq.Expressions;

namespace Sim.Domain.Interface.IService
{
    using Entity;
    public interface IServiceEmpregos : IServiceBase<Empregos>
    {   
        Task<Empregos> GetEmpregoByIdAsync(Guid id);
        Task<IEnumerable<Empregos>> DoListAsync(Expression<Func<Empregos, bool>>? filter = null);
    }
}
