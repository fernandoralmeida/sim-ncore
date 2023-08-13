using Sim.Domain.Entity;
using Sim.Application.Indicadores.VModel;

namespace Sim.Application.Indicadores.Interfaces;

public interface IAppIndicadores {
    Task<VmRAtendimentos> DoAtendimentosAsync(IEnumerable<EAtendimento> atendimentos);
}