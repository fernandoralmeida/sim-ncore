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
            Input = new();
        }

        public IEnumerable<Empregos> ListaEmpregos { get; set; }

        public async Task OnGetAsync() {            
            ListaEmpregos = await _appempregos.DoListAsync(
                s => s.Status == Empregos.EStatus.Finalizada.ToString() || 
                s.Status == "Finalizado");
        }

        public async Task OnPostAsync(){
            ListaEmpregos = await _appempregos.DoListAsync(
                e => e.Empresa.CNPJ.Contains(InputSearch) ||
                e.Empresa.Nome_Empresarial.Contains(InputSearch) ||
                e.Empresa.Atividade_Principal.Contains(InputSearch) ||
                e.Pessoa.CPF.Contains(InputSearch) ||
                e.Pessoa.Nome.Contains(InputSearch) ||
                e.Ocupacao.Contains(InputSearch) ||
                e.Inclusivo.Contains(InputSearch) ||
                e.Status.Contains(InputSearch) ||
                e.Genero.Contains(InputSearch));
        }
    }
}