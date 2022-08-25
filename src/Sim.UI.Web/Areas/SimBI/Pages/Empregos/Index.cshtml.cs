using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Areas.Empregos;

[Authorize]
public class IndexModel : PageModel {
    
    private readonly IAppServiceBIEmpregos _appServiceBiEmpregos;
   
    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public string Ano {get; set;}

    public KeyValuePair<string, int> VagasAtivas { get; set; }
    public KeyValuePair<string, int> VagasAcumuladas { get; set; }
    public KeyValuePair<string, int> VagasFinalizadas { get; set; }
    public IEnumerable<KeyValuePair<string, int>> VagasGeneros { get; set; }
    public IEnumerable<KeyValuePair<string, int>> VagasGenerosAcumuladas { get; set; }
    public IEnumerable<KeyValuePair<string, int>> ListaOcupacoes { get; set; }
    public IEnumerable<(string month, int valor, string percent)> ListVagasByMonth { get; set; }
    public IEnumerable<(string setor, int valor, string percent)> ListVagasBySetor { get; set; }

    public IndexModel(IAppServiceBIEmpregos appServiceBiEmpregos){
        _appServiceBiEmpregos = appServiceBiEmpregos;
    }

    public async Task OnGetAsync() {
        Ano = DateTime.Now.Year.ToString();
        VagasAtivas = await _appServiceBiEmpregos.DoEmpregosAtivos(DateTime.Now.Year);
        VagasFinalizadas = await _appServiceBiEmpregos.DoEmpregosFinalizados(DateTime.Now.Year);
        VagasAcumuladas = await _appServiceBiEmpregos.DoEmpregosAtivosAcumulado(DateTime.Now.Year);
        VagasGeneros = await _appServiceBiEmpregos.DoListEmpregosAtivosByGenero(DateTime.Now.Year);
        VagasGenerosAcumuladas = await _appServiceBiEmpregos.DoListEmpregosAtivosByGeneroAcumulado(DateTime.Now.Year);
        ListaOcupacoes = await _appServiceBiEmpregos.DoListOcupacoes(DateTime.Now.Year);
        ListVagasByMonth = await _appServiceBiEmpregos.DoListVagasByMonth(DateTime.Now.Year);
        ListVagasBySetor = await _appServiceBiEmpregos.DoListVagasBySetor(DateTime.Now.Year);
    }

    public async Task OnPostAsync() {
        if(Ano.All(char.IsDigit)) {
            if(Convert.ToInt32(Ano) > 0) {
                VagasAtivas = await _appServiceBiEmpregos.DoEmpregosAtivos(Convert.ToInt32(Ano));
                VagasFinalizadas = await _appServiceBiEmpregos.DoEmpregosFinalizados(DateTime.Now.Year);
                VagasAcumuladas = await _appServiceBiEmpregos.DoEmpregosAtivosAcumulado(Convert.ToInt32(Ano));
                VagasGeneros = await _appServiceBiEmpregos.DoListEmpregosAtivosByGenero(Convert.ToInt32(Ano));
                VagasGenerosAcumuladas = await _appServiceBiEmpregos.DoListEmpregosAtivosByGeneroAcumulado(Convert.ToInt32(Ano));
                ListaOcupacoes = await _appServiceBiEmpregos.DoListOcupacoes(Convert.ToInt32(Ano));
                ListVagasByMonth = await _appServiceBiEmpregos.DoListVagasByMonth(DateTime.Now.Year);
                ListVagasBySetor = await _appServiceBiEmpregos.DoListVagasBySetor(DateTime.Now.Year);
           }
        }
    }
}