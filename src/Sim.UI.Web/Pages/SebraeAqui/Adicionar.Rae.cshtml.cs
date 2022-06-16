using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sim.UI.Web.Pages.SebraeAqui
{
    using Sim.Application.Shared.Interface;
    using Sim.Cross.Identity;
    using Sim.Domain.SDE.Entity;

    [Authorize(Roles = "Administrador,M_Sebrae")]

    public class AdicionarRaeModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;

        public AdicionarRaeModel(IAppServiceAtendimento appServiceAtendimento)
        {
            _appServiceAtendimento = appServiceAtendimento;
        }

        [BindProperty(SupportsGet = true)]
        public Pages.Atendimento.InputModelAtendimento Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {

            var atendimemnto_ativo = Task.Run(() => _appServiceAtendimento.GetAtendimento(Input.Id));

            var atendimento = await atendimemnto_ativo;

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

                var t = Task.Run(() =>
                {
                    var atsebrae = _appServiceAtendimento.GetById(Input.Id);

                    var sebrae = new RaeSebrae() { Id = new Guid(), RAE = Input.Sebrae.RAE };

                    atsebrae.Sebrae = sebrae;

                    _appServiceAtendimento.Update(atsebrae);

                });

                await t;
                return RedirectToPage("./Index");

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }

        }
    }
}
