using Sim.Domain.Entity;
using Sim.Application.Indicadores.VModel;
using System.Linq.Expressions;

namespace Sim.Application.Indicadores.Interfaces;

public interface IAppIndicadores {
    Task<VmRAtendimentos> DoAtendimentosAsync(Expression<Func<EAtendimento, bool>>? filter = null);
}