using System.Linq.Expressions;
using Sim.Domain.Sebrae.Model;

namespace Sim.Domain.Sebrae.Interfaces;

public interface IRepositorySimples : IRepositoryBase<ESimples>
{
    Task<IEnumerable<ESimples>> DoListAsync(Expression<Func<ESimples, bool>>? filter = null);
}