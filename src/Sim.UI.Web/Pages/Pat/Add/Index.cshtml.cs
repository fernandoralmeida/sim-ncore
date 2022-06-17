using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.Pat.Add
{
    [Authorize(Roles = "Administrador,M_Pat")]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IAppServiceEmpregos _appServiceEmpregos;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public IndexModel(IAppServiceEmpresa appServiceEmpresa,
            IAppServiceEmpregos appServiceEmpregos) {
        
            _appServiceEmpresa = appServiceEmpresa;
            _appServiceEmpregos = appServiceEmpregos;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Input = new()
            {
                Data = DateTime.Now,
                Empresa = await _appServiceEmpresa.GetIdAsync(id),
                Salario = "0,00"
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page(); 

            var emprego = new Empregos()
            {
                Empresa = await _appServiceEmpresa.GetIdAsync(Input.Empresa.Id),
                Data = Input.Data,
                Experiencia = Input.Experiencia,
                Vagas = Input.Vagas,
                Ocupacao = Input.Ocupacao,
                Pagamento = Input.Pagamento,
                Salario = Convert.ToDecimal(Input.Salario),
                Status = Input.Status
            };

            await _appServiceEmpregos.AddAsync(emprego);

            return RedirectToPage("/Pat/Index");
        }
    }
}
