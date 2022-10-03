using Sim.Domain.Entity;
namespace Sim.Application.Interfaces;
public interface IAppServicePrefeitura : IAppServiceBase<EPrefeitura> {
    Task<EPrefeitura> GetByIdAsync(Guid id);
}
