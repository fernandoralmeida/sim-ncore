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

    public async Task<KeyValuePair<string, int>> DoEmpregosAtivos(int ano)
    {
        return await _serviceBI.DoEmpregosAtivos(ano);
    }

    public async Task<KeyValuePair<string, int>> DoEmpregosAtivosAcumulado(int ano)
    {
        return await _serviceBI.DoEmpregosAtivosAcumulado(ano);
    }

    public async Task<KeyValuePair<string, int>> DoEmpregosFinalizados(int ano)
    {
        return await _serviceBI.DoEmpregosFinalizados(ano);
    }

    public async Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByGenero(int ano)
    {
        return await _serviceBI.DoListEmpregosAtivosByGenero(ano);
    }

    public async Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByGeneroAcumulado(int ano)
    {
        return await _serviceBI.DoListEmpregosAtivosByGeneroAcumulado(ano);
    }

    public async Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByInclusao(int ano)
    {
        return await _serviceBI.DoListEmpregosAtivosByInclusao(ano);
    }

    public async Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByInclusaoAcumulado(int ano)
    {
        return await _serviceBI.DoListEmpregosAtivosByInclusaoAcumulado(ano);
    }

    public async Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByTipo(int ano)
    {
        return await _serviceBI.DoListEmpregosAtivosByTipo(ano);
    }

    public async Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByTipoAcumulado(int ano)
    {
        return await _serviceBI.DoListEmpregosAtivosByTipoAcumulado(ano);
    }

    public async Task<IEnumerable<KeyValuePair<string, int>>> DoListOcupacoes(int ano)
    {
        return await _serviceBI.DoListOcupacoes(ano);
    }
}