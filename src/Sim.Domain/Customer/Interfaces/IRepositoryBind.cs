using System.Linq.Expressions;
using Sim.Domain.Customer.Models;

namespace Sim.Domain.Customer.Interfaces;

public interface IRepositoryBind : IRepositoryBase<EBind>
{
    Task<IEnumerable<EBind>> DoListAsync(Expression<Func<EBind, bool>>? param = null);
}