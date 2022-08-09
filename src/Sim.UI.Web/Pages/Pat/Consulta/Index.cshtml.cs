using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using Sim.UI.Web.Functions;

namespace Sim.UI.Web.Pages.Pat.Consulta{

    [Authorize(Roles = "Administrador,M_Pat,Admin_Pat")]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpregos _appempregos;        

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public string InputSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public string InputTipo { get; set; }

        public IndexModel(IAppServiceEmpregos appServiceEmpregos){
            _appempregos = appServiceEmpregos;
        }

        public IEnumerable<Empregos> ListaEmpregos { get; set; }

        public async Task OnGetAsync(string? id, string? src) {            
            InputTipo = id ?? "Consultar";            
            if(id == "cnpj" && string.IsNullOrEmpty(src))
                src = src.MaskRemove();
                
            InputSearch = src;
            ListaEmpregos = await _appempregos.ListEmpregosAsync(InputSearch);
        }

        public async Task<IActionResult> OnPostAsync(string? id, string? src){
            InputTipo = id;            
            if(id == "cnpj" && string.IsNullOrEmpty(src))
                src = src.MaskRemove();
                
            InputSearch = src;
            ListaEmpregos = await _appempregos.ListEmpregosAsync(InputSearch);

            return RedirectToPage("");
        }
    }
}