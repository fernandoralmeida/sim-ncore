using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Sim.UI.Web.Pages.Pat
{
    using Sim.Application.SDE.Interface;
    using Sim.Domain.SDE.Entity;


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
        public void OnGet()
        {
            ListaEmpregos = (ICollection<Empregos>)appEmpregos.GetAllEmpregosAsync().Result;
        }

        public void OnPost()
        {
            ListaEmpregos = appEmpregos.GetAllEmpregosAsync(CNPJ).Result;
            ListaEmpresas = Task.Run(() => appEmpresa.ConsultaByCNPJ(CNPJ)).Result;
        }
    }
}
