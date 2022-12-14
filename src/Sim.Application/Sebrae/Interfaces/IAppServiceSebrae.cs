using Sim.Domain.Sebrae.Model;
using Sim.Domain.Entity;
using Sim.Domain.Evento.Model;

namespace Sim.Application.Sebrae.Interfaces;

public interface IAppServiceSebrae {
    Task<EReports> DoReportAsync(IEnumerable<EAtendimento> atendimentos, IEnumerable<EEvento> eventos);
}