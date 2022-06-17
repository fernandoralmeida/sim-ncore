using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Areas.Settings.Pages.Common
{
    public class CanalModel : PageModel
    {
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceSecretaria _appServiceSecretaria;
        private readonly IAppServiceCanal _appServiceCanal;
        public CanalModel(IAppServiceSetor appServiceSetor,
            IAppServiceSecretaria appServiceSecretaria,
            IAppServiceCanal appServiceCanal)
        {
            _appServiceSetor = appServiceSetor;
            _appServiceSecretaria = appServiceSecretaria;
            _appServiceCanal = appServiceCanal;
        }

        
        public class InputModel
        {
            [Key]
            [HiddenInput(DisplayValue = false)]
            public Guid Id { get; set; }

            [Required]
            [DisplayName("Canal")]
            public string Nome { get; set; }

            [DisplayName("Secretaria")]
            public Secretaria Secretaria { get; set; } //Secretaria

            [DisplayName("Setor")]
            public Setor Setor { get; set; } //Setor

            [DisplayName("Ativo")]
            public bool Ativo { get; set; }
            public virtual ICollection<Canal> Listar { get; set; }
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
            
            var set = await _appServiceSetor.ListAllAsync();
            
            var ca = await _appServiceCanal.ListAllAsync();
            

            Input = new InputModel()
            {
                Listar = ca.ToList(),
                Ativo = true
            };

            if (sec != null)            
                Secretarias = new SelectList(sec, nameof(Secretaria.Id), nameof(Secretaria.Nome), null);
            

            if (set != null)
                Setores = new SelectList(set, nameof(Setor.Id), nameof(Setor.Nome), null);
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
                    var set = await _appServiceSetor.GetIdAsync(SetorSelecionado);

                    var input = new Canal()
                    {
                        Nome = Input.Nome,
                        Secretaria = sec,
                        Setor = set,
                        Ativo = true
                    };

                    await _appServiceCanal.AddAsync(input);

                    Input.Nome = string.Empty;
                }
                await OnLoad();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar incluír novo canal!" + "\n" + ex.Message;

            }

        }

        public async Task OnPostRemoveAsync(Guid id)
        {
            try
            {

                var t = Task.Run(async () =>
                {

                    var canal = await _appServiceCanal.GetIdAsync(id);                  

                    await _appServiceCanal.RemoveAsync(canal);

                });

                await t;
                await OnLoad();

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar remover canal!" + "\n" + ex.Message;

            }
        }
    }


}
