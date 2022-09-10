using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Cnpj.Interfaces;
using Sim.Domain.Cnpj.Entity;

namespace Sim.UI.Web.Areas.SimBI.Pages.Empresas;

[Authorize]
public class MappingModel : PageModel {
    private readonly IAppServiceCnpj _appEmpresa;
    [TempData]
    public string StatusMessage { get; set; }

    public IEnumerable<EMapping> ListasEmpresas { get; set;}

    public string MunicipioSelecionado {get;set;}
    public MappingModel (IAppServiceCnpj appServiceCnpj) {
        _appEmpresa = appServiceCnpj;        
    }

    public async Task OnGetAsync(string? m) {
        StatusMessage = ""; 
        if(string.IsNullOrEmpty(m))            
            MunicipioSelecionado ="6607";
        else
            MunicipioSelecionado = m;
        ListasEmpresas = await _appEmpresa.DoListMappingEmpresasAsync(MunicipioSelecionado); 
    }
    
}