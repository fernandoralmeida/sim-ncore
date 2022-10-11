using System.Linq.Expressions;

namespace Sim.Domain.Evento.Interfaces.Service
{    
    using Model;
    public interface IServiceParceiro : IServiceBase<EParceiro>
    {
        Task<IEnumerable<EParceiro>> ListParceirosAsync(string owner);
        Task<EParceiro> GetIdAsync(Guid id);
        Task<IEnumerable<EParceiro>> ListAllAsync();
        Task<IEnumerable<EParceiro>> DoListAsync(Expression<Func<EParceiro, bool>>? filter = null);
    }
}
