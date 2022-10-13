using System.Linq.Expressions;

namespace Sim.Domain.Evento.Interfaces.Service
{
        using Model;
    public interface IServiceEvento : IServiceBase<EEvento>
    {
        int LastCodigo();
        Task<IEnumerable<(string Mes, int Qtde, IEnumerable<EEvento>)>> ListEventosPorMesAsync(IEnumerable<EEvento> eventos);
        Task<EEvento> GetIdAsync(Guid id);
        Task<IEnumerable<EEvento>> DoListAsync(Expression<Func<EEvento, bool>>? filter = null);
    }
}
