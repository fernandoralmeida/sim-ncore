using Sim.Domain.Sebrae.Model;
using Sim.Domain.Entity;
using Sim.Domain.Evento.Model;
using System.Linq.Expressions;
using Sim.Application.Sebrae.Views;

namespace Sim.Application.Sebrae.Interfaces;

public interface IAppServiceSebrae {
    Task<EReports> DoReportAsync(IEnumerable<EAtendimento> atendimentos, IEnumerable<EEvento> eventos);
    Task<(MemoryStream StreamFile, string ContentType, string Name)>DoExport(IEnumerable<EAtendimento> atendimentos, string user);
}