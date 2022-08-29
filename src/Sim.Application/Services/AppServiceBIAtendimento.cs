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
    public async Task<EChartDual> DoAsync(int ano)
    {
        return await _atendimento.DoAsync(ano);
    }

    public async Task<IEnumerable<EChart>> DoListClientesAsync(int ano)
    {
        return await _atendimento.DoListClientesAsync(ano);
    }

    public async Task<IEnumerable<EChartDual>> DoListSetorAsync(int ano)
    {
        return await _atendimento.DoListSetorAsync(ano);
    }

    public async Task<IEnumerable<EChartDual>> DoListUserAsync(int ano)
    {
        return await _atendimento.DoListUserAsync(ano);
    }

    public async Task<IEnumerable<EChart>> DoListServiceAsync(int ano)
    {
        return await _atendimento.DoListServiceAsync(ano);
    }
}