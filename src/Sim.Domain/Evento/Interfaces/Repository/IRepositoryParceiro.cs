namespace Sim.Domain.Evento.Interfaces.Repository;
using Model;
public interface IRepositoryParceiro: IRepositoryBase<EParceiro>
{
    Task<IEnumerable<EParceiro>> ListParceirosAsync(string owner);
    Task<EParceiro> GetIdAsync(Guid id);
    Task<IEnumerable<EParceiro>> ListAllAsync();
}

