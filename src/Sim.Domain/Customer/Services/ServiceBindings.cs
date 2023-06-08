using System.Linq.Expressions;
using Sim.Domain.Customer.Interfaces;
using Sim.Domain.Customer.Models;

namespace Sim.Domain.Customer.Services;

public class ServiceBindings : ServiceBase<EBindings>, IServiceBindings
{
    public readonly IRepositoryBindings _repository;

    public ServiceBindings(IRepositoryBindings repository)
    : base(repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EBindings>> DoListAsync(Expression<Func<EBindings, bool>>? param = null)
    {
        return await _repository.DoListAsync(param);
    }
}