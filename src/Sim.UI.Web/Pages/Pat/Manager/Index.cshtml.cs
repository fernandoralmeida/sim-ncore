using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.Pat.Manager
{
    [Authorize(Roles = $"{Web.Areas.Admin.Pages.Admin.Global},SEDEMPI Pat")]
    public class IndexModel : PageModel
    {        
        private readonly IAppServiceEmpregos _appServiceEmpregos;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public string InclusivasSelecionadas { get; set; }

        public IndexModel(IAppServiceEmpregos appServiceEmpregos)
        {
            _appServiceEmpregos = appServiceEmpregos;
        }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var e = await _appServiceEmpregos.GetEmpregoByIdAsync(id);
            Input = new()
            {
                Id = e.Id,
                Empresa = e.Empresa,
                Pessoa = e.Pessoa,
                Ocupacao = e.Ocupacao,
                Vagas = e.Vagas,
                Salario = e.Salario.ToString(),
                Pagamento = e.Pagamento,
                Experiencia = e.Experiencia,
                Status = e.Status,
                Inclusiva = e.Inclusivo,
                Genero = e.Genero,
                Data = e.Data
            };

            return Page();
        }

        public async Task<IActionResult> OnPostExclrAsync()
        {
            await _appServiceEmpregos.RemoveAsync(new Empregos()
            {
                Id = Input.Id,
                Data = Input.Data,
                Experiencia = Input.Experiencia,
                Vagas = Input.Vagas,
                Ocupacao = Input.Ocupacao,
                Pagamento = Input.Pagamento,
                Salario = Convert.ToDecimal(Input.Salario),
                Inclusivo = Input.Inclusiva,
                Genero = Input.Genero,
                Status = Input.Status
            });

            return RedirectToPage("/Pat/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                StatusMessage = "Alerta: Verifique se o formulário foi preenchido corretamente!";
                return Page(); 
            }               

            await _appServiceEmpregos.UpdateAsync(new Empregos()
            {
                Id = Input.Id,
                Data = Input.Data,
                Experiencia = Input.Experiencia,
                Vagas = Input.Vagas,
                Ocupacao = Input.Ocupacao,
                Pagamento = Input.Pagamento,
                Inclusivo = InclusivasSelecionadas,
                Genero = Input.Genero,
                Salario = Convert.ToDecimal(Input.Salario),
                Status = Input.Status
            });

            return RedirectToPage("/Pat/Index");
        }
    }
}
