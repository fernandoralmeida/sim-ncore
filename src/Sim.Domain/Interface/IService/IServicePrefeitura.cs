using Sim.Domain.Entity;

namespace Sim.Domain.Interface.IService;
public interface IServicePrefeitura : IServiceBase<EPrefeitura> {
    Task<EPrefeitura> GetByIdAsync(Guid id);
}