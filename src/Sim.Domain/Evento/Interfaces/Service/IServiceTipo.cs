using System.Linq.Expressions;

namespace Sim.Domain.Evento.Interfaces.Service
{
    using Model;
    public interface IServiceTipo : IServiceBase<ETipo>
    {
        Task<ETipo> GetIdAsync(Guid id);
        Task<IEnumerable<ETipo>> DoListAsync(Expression<Func<ETipo, bool>>? filter = null);
    }
}
