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

    public async Task<IEnumerable<EChart>> DoListSetorPercentAsync(int ano)
    {
        return await _atendimento.DoListSetorPercentAsync(ano);
    }

    public async Task<IEnumerable<EChartDual>> DoListMonthAsync(int ano)
    {
        return await _atendimento.DoListMonthAsync(ano);
    }

    public async Task<IEnumerable<EChartDual>> DoListCanalAsync(int ano)
    {
        return await _atendimento.DoListCanalAsync(ano);
    }

    public async Task<IEnumerable<EChart>> DoListCanalPercentAsync(int ano)
    {
        return await _atendimento.DoListCanalPercentAsync(ano);
    }
}