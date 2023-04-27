using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Cnpj.Interfaces;
using Sim.Domain.Cnpj.Entity;

namespace Sim.UI.Web.Areas.SimBI.Pages.Empresas;

[Authorize]
public class ListaModel : PageModel
{
    private readonly IAppServiceCnpj _appEmpresa;

    [BindProperty(SupportsGet = true)]
    public IEnumerable<BaseReceitaFederal> ListEmpresas { get; set; }
   
    [TempData]
    public string StatusMessage { get; set; }

    public ListaModel(IAppServiceCnpj appEmpresa)
    {
        _appEmpresa = appEmpresa;
        ListEmpresas = new List<BaseReceitaFederal>();
    }

    public async Task OnGetAsync(string c1, string c2, string m)
    {
        StatusMessage = "";

        ListEmpresas = await _appEmpresa.DoListAsync(
                s => s.CnaeFiscalPrincipal.CompareTo(c1) >= 0 &&
                s.CnaeFiscalPrincipal.CompareTo(c2) <= 0 && 
                s.Municipio == m && 
                s.SituacaoCadastral == "02");        
    }
}

