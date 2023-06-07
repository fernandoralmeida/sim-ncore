using System.Linq.Expressions;
using Sim.Domain.Customer.Models;

namespace Sim.Application.Customer.Interfaces;

public interface IAppServiceBind : IAppServiceBase<EBind>
{
    Task<IEnumerable<EBind>> DoListAsync(Expression<Func<EBind, bool>> param = null);
}