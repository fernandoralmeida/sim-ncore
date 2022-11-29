using Sim.Domain;
using Sim.Domain.BancoPovo.Models;
using Sim.Domain.BancoPovo.Interfaces;
using Sim.Application.BancoPovo.Interfaces;
using System.Linq.Expressions;

namespace Sim.Application.BancoPovo.Services;
public class AppServiceContrato : AppServiceBase<EContrato>, IAppServiceContratos
{
    private readonly IServiceContratos _contrato;
    public AppServiceContrato(IServiceContratos serviceBase) : base(serviceBase)
    {
        _contrato = serviceBase;
    }

    public async Task<IEnumerable<EContrato>> DoListAsync(Expression<Func<EContrato, bool>> filter = null)
    {
        return await _contrato.DoListAsync(filter);
    }

    public async Task<EContrato> GetIdAsync(Guid id)
    {
        return await _contrato.GetIdAsync(id);
    }
}