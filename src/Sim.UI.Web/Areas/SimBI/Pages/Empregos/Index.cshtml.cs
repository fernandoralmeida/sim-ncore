using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Areas.Empregos;

[Authorize]
public class IndexModel : PageModel {
    
    private readonly IAppServiceBIEmpregos _appServiceBiEmpregos;
   
    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public string Ano {get; set;}

    public EChart VagasAtivas { get; set; }
    public EChart VagasAcumuladas { get; set; }
    public EChart VagasFinalizadas { get; set; }
    public IEnumerable<EChart> VagasGeneros { get; set; }
    public IEnumerable<EChart> VagasGenerosAcumuladas { get; set; }
    public IEnumerable<EChart> ListaOcupacoes { get; set; }
    public IEnumerable<EChart> ListVagasByMonth { get; set; }
    public IEnumerable<EChart> ListVagasBySetor { get; set; }

    public IndexModel(IAppServiceBIEmpregos appServiceBiEmpregos){
        _appServiceBiEmpregos = appServiceBiEmpregos;
    }

    public async Task OnGetAsync() {
        Ano = DateTime.Now.Year.ToString();
        VagasAtivas = await _appServiceBiEmpregos.DoEmpregosAtivos(DateTime.Now.Year);
        VagasFinalizadas = await _appServiceBiEmpregos.DoEmpregosFinalizados(DateTime.Now.Year);
        VagasAcumuladas = await _appServiceBiEmpregos.DoEmpregosAtivosAcumulado(DateTime.Now.Year);
        ListaOcupacoes = await _appServiceBiEmpregos.DoListOcupacoes(DateTime.Now.Year);
    }

    public async Task OnPostAsync() {
        if(Ano.All(char.IsDigit)) {
            if(Convert.ToInt32(Ano) > 0) {
                VagasAtivas = await _appServiceBiEmpregos.DoEmpregosAtivos(Convert.ToInt32(Ano));
                VagasFinalizadas = await _appServiceBiEmpregos.DoEmpregosFinalizados(DateTime.Now.Year);
                VagasAcumuladas = await _appServiceBiEmpregos.DoEmpregosAtivosAcumulado(Convert.ToInt32(Ano));
                ListaOcupacoes = await _appServiceBiEmpregos.DoListOcupacoes(Convert.ToInt32(Ano));
           }
        }
    }

    public async Task<JsonResult> OnGetEmpregosAtivosByGeneroAsync(){
        await _appServiceBiEmpregos.DoEmpregosAtivos(DateTime.Now.Year);
        return new JsonResult(await _appServiceBiEmpregos.DoListEmpregosAtivosByGenero(DateTime.Now.Year));        
    }

    public async Task<JsonResult> OnGetEmpregosAtivosByGeneroAcAsync(){
        await _appServiceBiEmpregos.DoEmpregosAtivos(DateTime.Now.Year);
        return new JsonResult(await _appServiceBiEmpregos.DoListEmpregosAtivosByGeneroAcumulado(DateTime.Now.Year));        
    }

    public async Task<JsonResult> OnGetAnualAsync(){
        await _appServiceBiEmpregos.DoEmpregosAtivos(DateTime.Now.Year);
        return new JsonResult(await _appServiceBiEmpregos.DoListVagasByMonth(DateTime.Now.Year));        
    }
    public async Task<JsonResult> OnGetSetoresAsync(){
        await _appServiceBiEmpregos.DoEmpregosAtivos(DateTime.Now.Year);
        return new JsonResult(await _appServiceBiEmpregos.DoListVagasBySetor(DateTime.Now.Year));        
    }
}