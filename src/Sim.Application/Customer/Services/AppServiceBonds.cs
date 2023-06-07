using System.Linq.Expressions;
using Sim.Application.Customer.Interfaces;
using Sim.Domain.Customer.Interfaces;
using Sim.Domain.Customer.Models;

namespace Sim.Application.Customer.Services;

public class AppServiceBind : AppServiceBase<EBind> , IAppServiceBind
{
    private readonly IServiceBind _repository;

    public AppServiceBind(IServiceBind repository)
        :base(repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EBind>> DoListAsync(Expression<Func<EBind, bool>> param = null)
        => await _repository.DoListAsync(param);
}   