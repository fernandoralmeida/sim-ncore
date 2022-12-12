using Sim.Domain.BancoPovo.Models;
using Sim.Domain.BancoPovo.Interfaces;
using Sim.Application.BancoPovo.Interfaces;
using System.Linq.Expressions;
using System.Globalization;

namespace Sim.Application.BancoPovo.Services;
public class AppServiceContrato : AppServiceBase<EContrato>, IAppServiceContratos
{
    private readonly IServiceContratos _contrato;
    public AppServiceContrato(IServiceContratos serviceBase) : base(serviceBase)
    {
        _contrato = serviceBase;
    }

    public async Task<IEnumerable<EContrato>> DoListAsync(Expression<Func<EContrato, bool>> filter = null)
    {
        return await _contrato.DoListAsync(filter);
    }

    public async Task<EReports> DoReportsAsync(IEnumerable<EContrato> lista)
    {        
        return await Task.Run(() => {
            var _ret = new EReports();
            var _ec = new EContrato();

            _ret.ContratosReprovados = new KeyValuePair<string, int>
                                        ("Reprovados", lista.Where(s => s.ContratosReprovados(s)).Count());

            _ret.ContratosLiquidados = new KeyValuePair<string, int>
                                        ("Liquidados", lista.Where(s => s.ContratosLiquidados(s)).Count());

            _ret.ContratosCancelados = new KeyValuePair<string, int>
                                        ("Cancelados", lista.Where(s => s.ContratosCancelados(s)).Count());

            _ret.ContratosRenegociados = new KeyValuePair<string, int>
                                        ("Renegociados", lista.Where(s => s.ContratosRenegociados(s)).Count());

            _ret.ContratosEmAnalise = new KeyValuePair<string, int>
                                        ("Em Análise", lista.Where(s => s.ContratosEmAnalise(s)).Count());

            _ret.ContratosAprovadosInadimplente = new KeyValuePair<string, int>
                                        ("Inadimplente", lista.Where(s => s.ContratosAprovadosInadimplente(s)).Count());        

            _ret.ContratosAprovadosRegulares = new KeyValuePair<string, int>
                                        ("Regulares", lista.Where(s => s.ContratosAprovadosRegulares(s)).Count());      

            _ret.ValorContratosAnalise = string.Format("{0} {1}", NumberFormatInfo.CurrentInfo.CurrencySymbol, _ec.ValorContratosAnalise(lista));
            _ret.ValorContratosInadimplentes = string.Format("{0} {1}", NumberFormatInfo.CurrentInfo.CurrencySymbol, _ec.ValorContratosInadimplentes(lista));
            _ret.ValorContratosRegulares = string.Format("{0} {1}", NumberFormatInfo.CurrentInfo.CurrencySymbol, _ec.ValorContratosRegulares(lista));
            _ret.ValorContratosRenegociados = string.Format("{0} {1}", NumberFormatInfo.CurrentInfo.CurrencySymbol, _ec.ValorContratosRenegociados(lista));

            _ret.TaxaInadimplencia = string.Format("{0} %", _ec.TaxaInadimplencia(lista));

            _ret.ListaContratos = lista;
            return _ret;
        });
    }

    public async Task<EContrato> GetIdAsync(Guid id)
    {
        return await _contrato.GetIdAsync(id);
    }
}