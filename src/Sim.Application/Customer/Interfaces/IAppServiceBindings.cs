using System.Linq.Expressions;
using Sim.Domain.Customer.Models;

namespace Sim.Application.Customer.Interfaces;

public interface IAppServiceBindings : IAppServiceBase<EBindings>
{
    Task<IEnumerable<EBindings>> DoListAsync(Expression<Func<EBindings, bool>> param = null);
}