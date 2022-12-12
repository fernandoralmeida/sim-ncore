using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.BancoPovo.ViewModel;
using Sim.Application.BancoPovo.Interfaces;

namespace Sim.UI.Web.Pages.BancoPovo.Reports;

[Authorize(Roles = "Administrador")]
public class IndexModel : PageModel
{
    private readonly IAppServiceContratos _appconotratos;

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    public VMReports InputView { get; set; }

    public IndexModel(IAppServiceContratos appServiceContratos) {
        _appconotratos = appServiceContratos;
    }

    public void OnGet() {
        InputView = new();
        InputView.DataInicial = new DateTime(year: DateTime.Now.Year, month: 1, day: 1);
        InputView.DataFinal = DateTime.Now;
    }

    public async Task OnPostAsync() {
        var _list = await _appconotratos.DoListAsync(s => s.Data >= InputView.DataInicial && s.Data <= InputView.DataFinal);
        InputView.Relatorios = await _appconotratos.DoReportsAsync(_list);
    }

    public async Task OnGetPreviewAsync(int? id, DateTime? datai, DateTime? dataf){
        try{
            var _list = await _appconotratos.DoListAsync(s => s.Data.Value.Date >= datai && s.Data.Value.Date <= dataf);
            InputView.Relatorios = await _appconotratos.DoReportsAsync(_list);      
            InputView.DataInicial = datai;
            InputView.DataFinal = dataf;
            switch(id){
                case 0:
                    InputView.Relatorios.ListaContratos = _list.Where(s => s.ContratosEmAnalise(s));
                    break;
                case 1:
                    InputView.Relatorios.ListaContratos = _list.Where(s => s.ContratosAprovadosRegulares(s));
                    break;
                case 2:
                    InputView.Relatorios.ListaContratos = _list.Where(s => s.ContratosAprovadosInadimplente(s));
                    break;
                case 3:
                    InputView.Relatorios.ListaContratos = _list.Where(s => s.ContratosLiquidados(s));
                    break;
                case 4:
                    InputView.Relatorios.ListaContratos = _list.Where(s => s.ContratosReprovados(s));
                    break;           
                case 5:
                    InputView.Relatorios.ListaContratos = _list.Where(s => s.ContratosCancelados(s));
                    break;
                case 6:
                    InputView.Relatorios.ListaContratos = _list.Where(s => s.ContratosRenegociados(s));
                    break;
                default:
                    break;
            }    
        }
        catch(Exception ex){
            StatusMessage = "Erro: " + ex.Message;
        }
   
    }
}

