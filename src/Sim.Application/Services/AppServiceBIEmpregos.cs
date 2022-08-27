using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;

namespace Sim.Application.Services;

public class AppServiceBIEmpregos : IAppServiceBIEmpregos
{
    private readonly IServiceBIEmpregos _serviceBI;
    public AppServiceBIEmpregos(IServiceBIEmpregos serviceBIEmpregos) {
        _serviceBI = serviceBIEmpregos;
    }

    public async Task<EChart> DoEmpregosAtivos(int ano)
    {
        return await _serviceBI.DoEmpregosAtivos(ano);
    }

    public async Task<EChart> DoEmpregosAtivosAcumulado(int ano)
    {
        return await _serviceBI.DoEmpregosAtivosAcumulado(ano);
    }

    public async Task<EChart> DoEmpregosFinalizados(int ano)
    {
        return await _serviceBI.DoEmpregosFinalizados(ano);
    }

    public async Task<IEnumerable<EChart>> DoListEmpregosAtivosByGenero(int ano)
    {
        return await _serviceBI.DoListEmpregosAtivosByGenero(ano);
    }

    public async Task<IEnumerable<EChart>> DoListEmpregosAtivosByGeneroAcumulado(int ano)
    {
        return await _serviceBI.DoListEmpregosAtivosByGeneroAcumulado(ano);
    }

    public async Task<IEnumerable<EChart>> DoListEmpregosAtivosByInclusao(int ano)
    {
        return await _serviceBI.DoListEmpregosAtivosByInclusao(ano);
    }

    public async Task<IEnumerable<EChart>> DoListEmpregosAtivosByInclusaoAcumulado(int ano)
    {
        return await _serviceBI.DoListEmpregosAtivosByInclusaoAcumulado(ano);
    }

    public async Task<IEnumerable<EChart>> DoListEmpregosAtivosByTipo(int ano)
    {
        return await _serviceBI.DoListEmpregosAtivosByTipo(ano);
    }

    public async Task<IEnumerable<EChart>> DoListEmpregosAtivosByTipoAcumulado(int ano)
    {
        return await _serviceBI.DoListEmpregosAtivosByTipoAcumulado(ano);
    }

    public async Task<IEnumerable<EChart>> DoListOcupacoes(int ano)
    {
        return await _serviceBI.DoListOcupacoes(ano);
    }

    public async Task<IEnumerable<EChart>> DoListVagasByMonth(int ano)
    {
        return await _serviceBI.DoListVagasByMonth(ano);
    }

    public async Task<IEnumerable<EChart>> DoListVagasBySetor(int ano)
    {
        return await _serviceBI.DoListVagasBySetor(ano);
    }
}