using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Areas.Settings.Pages.Common
{
    public class SetoresModel : PageModel
    {
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceSecretaria _appServiceSecretaria;
        public SetoresModel(IAppServiceSetor appServiceSetor, IAppServiceSecretaria appServiceSecretaria)
        {
            _appServiceSetor = appServiceSetor;
            _appServiceSecretaria = appServiceSecretaria;
        }

        public class InputModel
        {
            [Key]
            [HiddenInput(DisplayValue = false)]
            public Guid Id { get; set; }

            [Required]
            [DisplayName("Setor")]
            public string Nome { get; set; }

            [DisplayName("Secretaria")]
            public Secretaria Secretaria { get; set; } //Secretaria

            [DisplayName("Ativo")]
            public bool Ativo { get; set; }

            public virtual ICollection<Setor> Listar { get; set; }

        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public Guid ItemSelecionado { get; set; }

        public SelectList Secretarias { get; set; }

        private async Task OnLoad()
        {
            var s = await _appServiceSecretaria.ListAllAsync();
            var t = await _appServiceSetor.ListAllAsync();

            Input = new InputModel()
            {
                Listar = t.ToList(),
                Ativo = true
            };
            
            if(s != null)
                Secretarias = new SelectList(s, nameof(Secretaria.Id), nameof(Secretaria.Nome), null);
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

                    var sec = await _appServiceSecretaria.GetIdAsync(ItemSelecionado);

                    var input = new Setor()
                    {
                        Nome = Input.Nome,
                        Secretaria = sec,
                        Ativo = true
                    };

                    await _appServiceSetor.AddAsync(input);
                }

                await OnLoad();
            }
            catch(Exception ex)
            {
                StatusMessage = "Erro ao tentar incluír novo setor!" + "\n" + ex.Message;
            }

        }

        public async Task OnPostRemoveAsync(Guid id)
        {
            try
            {
                var set = await _appServiceSetor.GetIdAsync(id);
                await _appServiceSetor.RemoveAsync(set);
                await OnLoad();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar remover setor!" + "\n" + ex.Message;
            }

        }
    }
}
