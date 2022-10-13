using Sim.Domain.Entity;
using System.Linq.Expressions;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceEmpregos : IAppServiceBase<Empregos>
    {
        Task<Empregos> GetEmpregoByIdAsync(Guid id);
        Task<IEnumerable<Empregos>> DoListAsync(Expression<Func<Empregos, bool>>? filter = null);
    }
}
