using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using Sim.Application.Interfaces;

namespace Sim.UI.Web.Pages.SebraeAqui
{
    [Authorize(Roles = "Administrador,M_Sebrae")]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;

        public IndexModel(IAppServiceAtendimento appServiceAtendimento)
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
            public IEnumerable<Domain.Entity.Atendimento> ListaAtendimentosNaoLancados { get; set; }
        }

        private async Task LoadAsync()
        {
            Input.ListaAtendimentosNaoLancados = await _appServiceAtendimento.ListRaeNaoLancadosAsync(User.Identity.Name);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            await LoadAsync();
            if (!Input.ListaAtendimentosNaoLancados.Any())
            {
                StatusMessage = string.Format("Não há atendimentos para lançar");
            }
            return Page();
        }
        public JsonResult OnGetPreview(string id)
        {
            return new JsonResult(_appServiceAtendimento.GetAtendimentoAsync(new Guid(id)).Result);
        }
    }
}
