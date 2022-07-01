using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Pages.SebraeAqui.Rae
{


    [Authorize(Roles = "Administrador,M_Sebrae")]
    public class LancadosModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;

        public LancadosModel(IAppServiceAtendimento appServiceAtendimento)
        {
            _appServiceAtendimento = appServiceAtendimento;
            Input = new()
            {
                DataAtendimento = DateTime.Now.Date
            };
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DataType(DataType.Date)]
            public DateTime? DataAtendimento { get; set; }

            public IEnumerable<Domain.Entity.Atendimento> ListaAtendimento { get; set; }
        }

        private async Task LoadAsync()
        {

            Input.ListaAtendimento = await _appServiceAtendimento.ListRaeLancadosAsync(User.Identity.Name);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            await LoadAsync();
            if (!Input.ListaAtendimento.Any())
            {
                StatusMessage = string.Format("Erro: Não há atendimentos para do {0}", Input.DataAtendimento.Value.Date);
            }

            return Page();
        }

        public async Task<JsonResult> OnGetPreview(string id)
        {
            return new JsonResult(await _appServiceAtendimento
                .GetAtendimentoAsync(new Guid(id)));
        }
    }
}
