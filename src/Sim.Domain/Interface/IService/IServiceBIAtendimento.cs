using Sim.Domain.Entity;

namespace Sim.Domain.Interface.IService;

public interface IServiceBIAtendimento {

    Task<EChartDual> DoAsync(int ano);
    Task<IEnumerable<EChart>> DoListClientesAsync(int ano);
    Task<IEnumerable<EChartDual>> DoListSetorAsync(int ano);
    Task<IEnumerable<EChartDual>> DoListUserAsync(int ano);
    Task<IEnumerable<EChart>> DoListServiceAsync(int ano);
    
}