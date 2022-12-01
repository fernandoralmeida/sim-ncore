using System.Linq.Expressions;
using Sim.Domain.BancoPovo.Models;

namespace Sim.Domain.BancoPovo.Interfaces;

public interface IRepositoryRenegociacoes : IRepositoryBase<ERenegociacoes> {
    Task<ERenegociacoes> GetIdAsync(Guid id);
    Task<IEnumerable<ERenegociacoes>> DoListAsync(Expression<Func<ERenegociacoes, bool>>? filter = null);
}