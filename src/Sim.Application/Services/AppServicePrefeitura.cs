using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;
using Sim.Domain;

namespace Sim.Application.Services;
public class AppServicePrefeitura : AppServiceBase<EPrefeitura>, IAppServicePrefeitura
{
    private readonly IServicePrefeitura _appserviceprefeitura;
    public AppServicePrefeitura(IServicePrefeitura serviceBase) : base(serviceBase)  {
        _appserviceprefeitura = serviceBase;
    }

    public async Task<EPrefeitura> GetByIdAsync(Guid id) {
        return await _appserviceprefeitura.GetByIdAsync(id);   
    }
}