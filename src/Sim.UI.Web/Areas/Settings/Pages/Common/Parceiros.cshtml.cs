using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Areas.Settings.Pages.Common
{
    public class ParceirosModel : PageModel
    {

        private readonly IAppServiceSecretaria _appServiceSecretaria;
        private readonly IAppServiceParceiro _appServiceParceiro;
        public ParceirosModel(IAppServiceSecretaria appServiceSecretaria,
            IAppServiceParceiro appServiceParceiro)
        {
            _appServiceSecretaria = appServiceSecretaria;
            _appServiceParceiro = appServiceParceiro;
        }


        public class InputModel
        {
            [Key]
            [HiddenInput(DisplayValue = false)]
            public Guid Id { get; set; }

            [Required]
            [DisplayName("Parceiro")]
            public string Nome { get; set; }

            [DisplayName("Secretaria")]
            public Secretaria Secretaria { get; set; } //Secretaria

            [DisplayName("Ativo")]
            public bool Ativo { get; set; }
            public virtual ICollection<Parceiro> Listar { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public Guid SecretariaSelecionada { get; set; }

        [BindProperty]
        public Guid SetorSelecionado { get; set; }

        public SelectList Secretarias { get; set; }

        public SelectList Setores { get; set; }

        private async Task OnLoad()
        {
            var sec = await _appServiceSecretaria.ListAllAsync();
            var parc = await _appServiceParceiro.ListAllAsync();

            Input = new InputModel()
            {
                Listar = parc.ToList(),
                Ativo = true
            };

            if (sec != null)
                Secretarias = new SelectList(sec, nameof(Secretaria.Id), nameof(Secretaria.Nome), null);

        }

        public async Task<IActionResult> OnGetAsync()
        {
            await OnLoad();
            return Page();
        }

        public async Task OnPostAddAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var sec = await _appServiceSecretaria.GetIdAsync(SecretariaSelecionada);

                    var input = new Parceiro()
                    {
                        Nome = Input.Nome,
                        Secretaria = sec,
                        Ativo = true
                    };

                    await _appServiceParceiro.AddAsync(input);

                    Input.Nome = string.Empty;
                }
                await OnLoad();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar incluír novo parceiro!" + "\n" + ex.Message;

            }

        }

        public async Task OnPostRemoveAsync(Guid id)
        {
            try
            {
                var canal = await _appServiceParceiro.GetIdAsync(id);

                await _appServiceParceiro.RemoveAsync(canal);

                await OnLoad();

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar remover parceiro!" + "\n" + ex.Message;

            }
        }
    }
}
