using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using Sim.Domain.Entity;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Pages.Atendimento.Consultas
{


    [Authorize(Roles = "Admin_Global")]
    public class AtivosModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        public AtivosModel(IAppServiceAtendimento appServiceAtendimento)
        {
            _appServiceAtendimento = appServiceAtendimento;
            Input = new()
            {
                DataI = new DateTime(DateTime.Now.Year, 1, 1),
                DataF = DateTime.Now
            };
        }
        public async Task<IActionResult> OnGetAsync()
        {
            Input.ListaAtendimento = await _appServiceAtendimento.ListAtendimentosAtivosAsync();
            return Page();
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DataType(DataType.Date)]
            public DateTime? DataI { get; set; }

            [DataType(DataType.Date)]
            public DateTime? DataF { get; set; }

            public string CPF { get; set; }

            public string CNPJ { get; set; }

            public IEnumerable<EAtendimento> ListaAtendimento { get; set; }
        }

        public async Task OnPostListPendenciasAsync()
        {
            Input.ListaAtendimento = await _appServiceAtendimento.ListAtendimentosAtivosAsync();
        }
    }
}
