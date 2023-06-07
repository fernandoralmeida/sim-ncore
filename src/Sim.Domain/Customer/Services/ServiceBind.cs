using System.Linq.Expressions;
using Sim.Domain.Customer.Interfaces;
using Sim.Domain.Customer.Models;

namespace Sim.Domain.Customer.Services;

public class ServiceBind : ServiceBase<EBind>, IServiceBind
{
    public readonly IRepositoryBind _repository;

    public ServiceBind(IRepositoryBind repository)
    : base(repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EBind>> DoListAsync(Expression<Func<EBind, bool>>? param = null)
    {
        return await _repository.DoListAsync(param);
    }
}