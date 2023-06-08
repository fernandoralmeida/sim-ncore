using System.Linq.Expressions;
using Sim.Domain.Customer.Models;

namespace Sim.Domain.Customer.Interfaces;

public interface IServiceBindings : IServiceBase<EBindings>
{
    Task<IEnumerable<EBindings>> DoListAsync(Expression<Func<EBindings, bool>>? param = null);
}