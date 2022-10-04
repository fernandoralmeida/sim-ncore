using Sim.Domain.Entity;

namespace Sim.Domain.Interface.IRepository;
public interface IRepositoryPrefeitura : IRepositoryBase<EPrefeitura> {
    Task<EPrefeitura> GetByIdAsync(Guid id);
}