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
                new Locations(1, "Bhubaneswar","Bhubaneswar, Odisha", 20.296059, 85.824539),  
                new Locations(2, "Hyderabad","Hyderabad, Telengana", 17.387140, 78.491684),  
                new Locations(3, "Bengaluru","Bengaluru, Karnataka", 12.972442, 77.580643)  
            }; 

        StatusMessage = ""; 
        if(string.IsNullOrEmpty(m))            
            MunicipioSelecionado ="6607";
        else
            MunicipioSelecionado = m;

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
        public double Latitude { get; set; }  
        public double Longitude { get; set; }  
  
        public Locations(int locid, string title, string desc, double latitude, double longitude)  
        {  
            this.LocationId = locid;  
            this.Title = title;  
            this.Description = desc;  
            this.Latitude = latitude;  
            this.Longitude = longitude;  
        }  
    }  
}