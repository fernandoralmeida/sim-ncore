using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.Pat
{
    [Authorize(Roles = "Administrador,M_Pat")]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpregos appEmpregos;
        private readonly IAppServiceEmpresa appEmpresa;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public string CNPJ { get; set; }
        public IEnumerable<Empregos> ListaEmpregos { get; set; }
        public IEnumerable<Empresas> ListaEmpresas { get; set; }

        public IndexModel(IAppServiceEmpregos appServiceEmpregos,
            IAppServiceEmpresa appServiceEmpresa)
        {
            appEmpregos = appServiceEmpregos;
            appEmpresa = appServiceEmpresa;
        }
        public async Task OnGet()
        {
            ListaEmpregos = await appEmpregos.ListAllAsync();
        }

        public async Task OnPost()
        {
            ListaEmpresas = await appEmpresa.ConsultaCNPJAsync(CNPJ);

            StatusMessage = "";

            if (ListaEmpresas.Any())
            {
                ListaEmpregos = await appEmpregos.ListEmpregosAsync(CNPJ);
            }
            else
            {
                StatusMessage = "Alerta: Empresa não cadastrada!";
            }
        }
    }
}
