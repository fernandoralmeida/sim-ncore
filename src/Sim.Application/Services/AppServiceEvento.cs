using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Interfaces.Service;

namespace Sim.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Interfaces;
    public class AppServiceEvento : AppServiceBase<EEvento>, IAppServiceEvento
    {
        private readonly IServiceEvento _evento;
        public AppServiceEvento(IServiceEvento evento)
            :base(evento)
        {
            _evento = evento;
        }

        public async Task<IEnumerable<EEvento>> DoListAsync(Expression<Func<EEvento, bool>> filter = null)
        {
            return await _evento.DoListAsync(filter);
        }

        public async Task<EEvento> GetIdAsync(Guid id)
        {
            return await _evento.GetIdAsync(id);
        }

        public int LastCodigo()
        {
            return _evento.LastCodigo();
        }

        public async Task<IEnumerable<(string Mes, int Qtde, IEnumerable<EEvento>)>> ListEventosPorMesAsync(IEnumerable<EEvento> eventos)
        {
            return await _evento.ListEventosPorMesAsync(eventos);
        }
    }
}
