using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service;
public class ServicePrefeitura : ServiceBase<EPrefeitura>, IServicePrefeitura
{
    private readonly IRepositoryPrefeitura _repositoryPrefeitura;
    public ServicePrefeitura(IRepositoryPrefeitura repositoryBase) : base(repositoryBase) {
        _repositoryPrefeitura = repositoryBase;
    }

    public async Task<EPrefeitura> GetByIdAsync(Guid id)
    {
        return await _repositoryPrefeitura.GetByIdAsync(id);
    }
}