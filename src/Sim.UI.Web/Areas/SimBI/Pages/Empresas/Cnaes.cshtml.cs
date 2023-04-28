using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Cnpj.Interfaces;
using Sim.Domain.Cnpj.Entity;

namespace Sim.UI.Web.Areas.SimBI.Pages.Empresas
{
    [Authorize]
    public class CnaesModel: PageModel
    {
        private readonly IAppServiceCnpj _appEmpresa;
        
        public IEnumerable<BICnae> ListCnaes { get; set; }  

        [TempData]
        public string StatusMessage { get; set; }

        public string MunicipioSelecionado {get;set;}
        
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Municipio { get; set; }
            public string Ano { get; set; }
        }

        public CnaesModel(IAppServiceCnpj appEmpresa)
        {
            _appEmpresa = appEmpresa;
            ListCnaes = new List<BICnae>();
        }

        public async Task OnGetAsync(string m)
        {     
            StatusMessage = "";   

            if(string.IsNullOrEmpty(m))            
                MunicipioSelecionado ="6607";
            else
                MunicipioSelecionado = m;
                
            ListCnaes = await _appEmpresa.DoListBICnaeAsync(m);
        }

        public async Task<JsonResult> OnGetPreview(string ci, string cf, string m, string a)
        {
            return new JsonResult(await _appEmpresa.DoListCnaeEmpresasJsonAsync(ci, cf, m, a));
        }
        public async Task<JsonResult> OnGetSubClasses(string ci, string cf, string m)
        {
            return new JsonResult(await _appEmpresa
                .DoListCnaesAsync(s => 
                    s.CnaeFiscalPrincipal.CompareTo(ci) >= 0 && 
                    s.CnaeFiscalPrincipal.CompareTo(cf) <= 0 && 
                    s.Municipio == m && s.SituacaoCadastral == "02"));
        }
    }
}