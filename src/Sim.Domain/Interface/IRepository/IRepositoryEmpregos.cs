using System.Linq.Expressions;

namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositoryEmpregos : IRepositoryBase<Empregos>
    {
        Task<Empregos> GetEmpregoByIdAsync(Guid id);
        Task<IEnumerable<Empregos>> DoListAsync(Expression<Func<Empregos, bool>>? filter = null);
    }
}
