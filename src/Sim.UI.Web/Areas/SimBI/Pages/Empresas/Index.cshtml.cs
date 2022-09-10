using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Application.Cnpj.Interfaces;
using Sim.Domain.Cnpj.Entity;

namespace Sim.UI.Web.Areas.SimBI.Pages.Empresas
{ 
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceCnpj _appEmpresa;

        [BindProperty(SupportsGet = true)]
        public IEnumerable<BIEmpresas> ListEmpresas { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Municipio { get; set; }
            public string Ano { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IndexModel(IAppServiceCnpj appEmpresa)
        {
            _appEmpresa = appEmpresa;     
            Input=new();       
        }
        public async Task OnGetAsync(string m)
        {
            StatusMessage = "";

            if(string.IsNullOrEmpty(m))
                Input.Municipio = "6607";
            else
                Input.Municipio = m;

            Input.Ano= DateTime.Today.Year.ToString();
            
            ListEmpresas = await _appEmpresa.DoListBIEmpresasAsync(Input.Municipio, "Ativa", Input.Ano, "00");
        }

        public async Task OnPostAsync(string m)
        {
            if(string.IsNullOrEmpty(m))
                Input.Municipio = "6607";
            else
                Input.Municipio = m;
                
            ListEmpresas = await _appEmpresa.DoListBIEmpresasAsync(Input.Municipio, "Ativa", Input.Ano, "00");
        }
    }
}
