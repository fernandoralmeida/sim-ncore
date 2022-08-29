using Sim.Domain.Entity;

namespace Sim.Application.Interfaces;

public interface IAppServiceBIAtendimento {
    Task<EChartDual> DoListByAnoAsync(int ano);
    Task<IEnumerable<EChart>> DoListByClienteAsync(int ano);
    Task<IEnumerable<EChart>> DoListBySetorAsync(int ano);
    Task<IEnumerable<EChart>> DoListByUserAsync(int ano);
    Task<IEnumerable<EChart>> DoListByService(int ano);
}