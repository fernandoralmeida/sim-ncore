using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Pages.Atendimento
{

    [Authorize]
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
            [DisplayName("Data")]
            [DataType(DataType.Date)]
            public DateTime? DataAtendimento { get; set; }

            public IEnumerable<Domain.Entity.Atendimento> ListaAtendimento { get; set; }
        }

        private async Task LoadAsync(DateTime? date)
        {
            Input.DataAtendimento = date;
            Input.ListaAtendimento = await _appServiceAtendimento.ListMeusAtendimentosAsync(User.Identity.Name, date);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                await LoadAsync(Input.DataAtendimento);
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await LoadAsync(Input.DataAtendimento);

                if (!Input.ListaAtendimento.Any())
                {
                    StatusMessage = string.Format("Erro: N�o h� atendimentos para {0}", Input.DataAtendimento.Value.Date);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                Input.ListaAtendimento = new List<Domain.Entity.Atendimento>();
            }

            return Page();
        }

        public async Task<JsonResult> OnGetPreview(string id)
        {
            return new JsonResult(
                new List<Domain.Entity.Atendimento>
                {
                    await _appServiceAtendimento.GetAtendimentoAsync(new Guid(id))
                });
        }
    }
}