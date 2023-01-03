using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.SebraeAqui.Rae
{
    [Authorize(Roles = "Admin_Global,M_Sebrae,M_Sebrae_Admin")]

    public class LancarModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;

        public LancarModel(IAppServiceAtendimento appServiceAtendimento)
        {
            _appServiceAtendimento = appServiceAtendimento;
        }

        [BindProperty(SupportsGet = true)]
        public Pages.Atendimento.InputModelAtendimento Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Ano { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int y, Guid id)
        {
            Ano = y;
            var atendimento = await _appServiceAtendimento.GetAtendimentoAsync(id);

            Input = new()
            {
                Id = atendimento.Id,
                Protocolo = atendimento.Protocolo,
                Data = atendimento.Data,
                DataF = atendimento.DataF,
                Setor = atendimento.Setor,
                Canal = atendimento.Canal,
                Servicos = atendimento.Servicos,
                Descricao = atendimento.Descricao,
                Status = atendimento.Status,
                Ultima_Alteracao = atendimento.Ultima_Alteracao,
                Ativo = atendimento.Ativo,
                Owner_AppUser_Id = atendimento.Owner_AppUser_Id,
                Pessoa = atendimento.Pessoa,
                Empresa = atendimento.Empresa,
                Sebrae = atendimento.Sebrae
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                if (!ModelState.IsValid)
                    return Page();

                var atsebrae = await _appServiceAtendimento.GetIdAsync(Input.Id);

                atsebrae.Sebrae = new RaeSebrae() { Id = new Guid(), RAE = Input.Sebrae.RAE }; ;

                await _appServiceAtendimento.UpdateAsync(atsebrae);

                return RedirectToPage("/SebraeAqui/Index", new { Src = Ano });

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }

        }
    }
}
