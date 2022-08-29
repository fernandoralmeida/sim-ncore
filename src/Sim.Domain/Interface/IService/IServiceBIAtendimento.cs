using Sim.Domain.Entity;

namespace Sim.Domain.Interface.IService;

public interface IServiceBIAtendimento {

    Task<EChartDual> DoListByAnoAsync(int ano);
    Task<IEnumerable<EChart>> DoListByClienteAsync(int ano);
    Task<IEnumerable<EChart>> DoListBySetorAsync(int ano);
    Task<IEnumerable<EChart>> DoListByUserAsync(int ano);
    Task<IEnumerable<EChart>> DoListByService(int ano);
    
}