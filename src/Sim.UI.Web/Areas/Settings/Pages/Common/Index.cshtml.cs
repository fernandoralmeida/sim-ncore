using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Areas.Settings.Pages.Common
{
    public class IndexModel : PageModel
    {
        private readonly IAppServiceSecretaria _appServiceSecretaria;
        public IndexModel(IAppServiceSecretaria appServiceSecretaria)
        {
            _appServiceSecretaria = appServiceSecretaria;
        }

        public class InputModel
        {
            [Key]
            [HiddenInput(DisplayValue = false)]
            public Guid Id { get; set; }

            [Required]
            [DisplayName("Secretaria")]
            public string Nome { get; set; }

            [DisplayName("Unidade Responsável")]
            public string Owner { get; set; } //Prefeitura

            [DisplayName("Ativo")]
            public bool Ativo { get; set; }

            public virtual ICollection<Secretaria> Listar { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        
        private async Task OnLoad()
        {
            var t = await _appServiceSecretaria.ListAllAsync();
            Input = new InputModel()
            {
                Listar = t.ToList(),
                Ativo = true
            };
        }

        public void OnGet()
        {
            OnLoad().Wait();
        }

        public async Task OnPostAddAsync()
        {
            if (ModelState.IsValid)
            {
                var input = new Secretaria()
                {
                    Nome = Input.Nome,
                    Owner = Input.Owner,
                    Ativo = true
                };

                await _appServiceSecretaria.AddAsync(input);
            }

            await OnLoad();
        }

        public async Task OnPostRemoveAsync(Guid id)
        {
            try
            {
                var sec = await _appServiceSecretaria.GetIdAsync(id);
                await _appServiceSecretaria.RemoveAsync(sec);

                await OnLoad();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar remover Secretaria!" + "\n" + ex.Message;
            }
        }
    }
}
