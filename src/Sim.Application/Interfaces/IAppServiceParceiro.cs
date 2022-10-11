using System.Linq.Expressions;
using Sim.Domain.Evento.Model;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceParceiro : IAppServiceBase<EParceiro>
    {
        Task<IEnumerable<EParceiro>> ListParceirosAsync(string owner);
        Task<EParceiro> GetIdAsync(Guid id);
        Task<IEnumerable<EParceiro>> ListAllAsync();
        Task<IEnumerable<EParceiro>> DoListAsync(Expression<Func<EParceiro, bool>>? filter = null);
    }
}
