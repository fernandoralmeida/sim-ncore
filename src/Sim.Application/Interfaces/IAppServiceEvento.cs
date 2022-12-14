using System.Linq.Expressions;
using Sim.Domain.Evento.Model;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceEvento : IAppServiceBase<EEvento>
    {
        int LastCodigo();
        Task<IEnumerable<(string Mes, int Qtde, IEnumerable<EEvento>)>> ListEventosPorMesAsync(IEnumerable<EEvento> eventos);
        Task<EEvento> GetIdAsync(Guid id);
        Task<IEnumerable<EEvento>> DoListAsync(Expression<Func<EEvento, bool>>? filter = null);
        Task<EBIEventos> DoBIEventosAsync(IEnumerable<EEvento> lista);

    }
}
