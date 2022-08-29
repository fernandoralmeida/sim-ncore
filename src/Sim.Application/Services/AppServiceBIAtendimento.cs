using Sim.Application.Interfaces;
using Sim.Domain.Interface.IService;

using Sim.Domain.Entity;

namespace Sim.Application.Services;
public class AppServiceBIAtendimento : IAppServiceBIAtendimento
{
    private IServiceBIAtendimento _atendimento;
    public AppServiceBIAtendimento(IServiceBIAtendimento serviceBIAtendimento) {
        _atendimento = serviceBIAtendimento;        
    }
    public async Task<EChartDual> DoListByAnoAsync(int ano)
    {
        return await _atendimento.DoListByAnoAsync(ano);
    }

    public async Task<IEnumerable<EChart>> DoListByClienteAsync(int ano)
    {
        return await _atendimento.DoListByClienteAsync(ano);
    }

    public async Task<IEnumerable<EChart>> DoListByService(int ano)
    {
        return await _atendimento.DoListByService(ano);
    }

    public async Task<IEnumerable<EChart>> DoListBySetorAsync(int ano)
    {
        return await _atendimento.DoListBySetorAsync(ano);
    }

    public async Task<IEnumerable<EChart>> DoListByUserAsync(int ano)
    {
        return await _atendimento.DoListByUserAsync(ano);
    }
}