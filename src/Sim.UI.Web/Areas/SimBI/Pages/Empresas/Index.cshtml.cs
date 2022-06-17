using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using Sim.Application.Cnpj.Interfaces;
using Sim.Domain.Cnpj.Entity;

namespace Sim.UI.Web.Areas.SimBI.Pages.Empresas
{ 
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceCnpj _appEmpresa;

        public SelectList ListaMunicipios { get; set; }

        [BindProperty(SupportsGet = true)]
        public IEnumerable<BIEmpresas> ListEmpresas { get; set; }

        [BindProperty(SupportsGet = true)]
        public IEnumerable<BICnae> ListCnaes { get; set; }

        [BindProperty(SupportsGet = true)]
        public IEnumerable<BaseReceitaFederal> ListCnaeTB { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DisplayName("Situa��o")]
            public string Situacao { get; set; }
            public string Municipio { get; set; }
            public string Mes { get; set; }
            public int Ano { get; set; }
            public string Modo { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IndexModel(IAppServiceCnpj appEmpresa)
        {
            _appEmpresa = appEmpresa;
        }

        private async Task LoadMunicipios()
        {

            var t = await _appEmpresa.ToListMicroRegiaoJahuAsync();

            if (t != null)
            {
                ListaMunicipios = new SelectList(t, nameof(Municipio.Codigo), nameof(Municipio.Descricao), null);
            }
        }

        private async Task LoadAsync()
        {
            await LoadMunicipios();

            if (Input.Modo == "Atividades")
                ListCnaes = await _appEmpresa.ToListBICnaeAsync(Input.Municipio);            

            else
                ListEmpresas = await _appEmpresa.ToListBIEmpresasAsync(Input.Municipio, Input.Situacao, Input.Ano.ToString(), Input.Mes);
        }

        public async Task OnGetAsync()
        {
            Input = new();
            Input.Municipio = "6607";
            Input.Situacao = "Ativa";
            Input.Ano = DateTime.Today.Year;
            Input.Mes = "00";      
            await LoadAsync();
        }

        public async Task OnPostAsync()
        {
            await LoadAsync();
        }

        public async Task<JsonResult> OnGetPreview(string c, string m, string a)
        {
            return new JsonResult(await _appEmpresa.ToListCnaeEmpresasJsonAsync(c, m, a));
        }       

    }
}
