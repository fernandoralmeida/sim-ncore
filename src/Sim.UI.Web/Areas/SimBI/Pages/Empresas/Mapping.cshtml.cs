using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Cnpj.Interfaces;
using Sim.Domain.Cnpj.Entity;


namespace Sim.UI.Web.Areas.SimBI.Pages.Empresas;

[Authorize]
public class MappingModel : PageModel {
    private readonly IAppServiceCnpj _appEmpresa;
    [TempData]
    public string StatusMessage { get; set; }
    public string Municipio { get; set; }
    public string MunicipioSelecionado {get;set;}
    [BindProperty(SupportsGet = true)]
    public string ZonaSelecionada { get; set; }
    public SelectList Zonas { get; set; }
    public IEnumerable<Locations> LocationList { get; set; }      
    public MappingModel (IAppServiceCnpj appServiceCnpj) {
        _appEmpresa = appServiceCnpj;        
    }

    public async Task OnGetAsync(string? m) {        

        LocationList = new List<Locations>() {  
                new Locations(1, "SEDEMPI", "Jau-SP", "Rua Treze de Maio, 347, Jau-SP"),  
                new Locations(1, "Prefeitura", "Jau-SP", "Rua Paisasndu, 444, Jau-SP"), 
                new Locations(1, "Igreja Matriz", "Jau-SP", "Rua Visconde do Rio Branco, 333, Jau-SP"), 
            }; 

        StatusMessage = ""; 
        if(string.IsNullOrEmpty(m))            
            MunicipioSelecionado ="6607";
        else
        {
            MunicipioSelecionado = m;
            var _ml = await _appEmpresa.DoListMicroRegiaoJahuAsync();
            var _c = _ml.Where(n => n.Codigo.Contains(m)).SingleOrDefault().Descricao;
            Municipio = $"{_c}-SP";
        }

        var _list = await _appEmpresa.DoListMappingZonasAsync(MunicipioSelecionado, "Ativa");
        Zonas =  new SelectList(_list);
    }

    public async Task<JsonResult> OnGetZonaAsync(string bro, string lgd, string mcp, string a) =>
        new JsonResult(await _appEmpresa.DoListZonaJsonAsync(zona: bro, municipio: mcp, situacao: a));

    public async Task<JsonResult> OnGetLAgrupadosAsync(string bro, string lgd, string mcp, string a) =>
        new JsonResult(await _appEmpresa.DoListMappingLogradourosAsync(zona: bro, municipio: mcp, situacao: a));
    
    public class Locations  
    {  
        public int LocationId { get; set; }  
        public string Title { get; set; }  
        public string Description { get; set; }  
        public string Location { get; set; }
  
        public Locations(int locid, string title, string desc, string location)  
        {  
            this.LocationId = locid;  
            this.Title = title;  
            this.Description = desc;  
            this.Location = location; 
        }  
    }  
}