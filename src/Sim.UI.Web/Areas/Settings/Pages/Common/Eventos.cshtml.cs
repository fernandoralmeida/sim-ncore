using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Areas.Settings.Pages.Common
{
    public class EventosModel : PageModel
    {

        private readonly IAppServiceTipo _appServiceTipo;
        public EventosModel(IAppServiceTipo appServiceTipo)
        {

            _appServiceTipo = appServiceTipo;
        }

        public class InputModel
        {
            [Key]
            [HiddenInput(DisplayValue = false)]
            public Guid Id { get; set; }

            [Required]
            [DisplayName("Tipo")]
            public string Nome { get; set; }

            [DisplayName("Tipo")]
            public string Tipo { get; set; } //Tipo

            [DisplayName("Ativo")]
            public bool Ativo { get; set; }

            public virtual ICollection<Tipo> Listar { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        private async Task OnLoad()
        {
            var eve = await _appServiceTipo.ListAllAsync();

            Input = new InputModel()
            {
                Listar = eve.ToList(),
                Ativo = true
            };
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
                    var input = new Tipo()
                    {
                        Nome = Input.Nome,
                        Owner = Input.Tipo,
                        Ativo = true
                    };

                    await _appServiceTipo.AddAsync(input);

                }
                await OnLoad();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar incluír novo tipo!" + "\n" + ex.Message;

            }

        }

        public async Task OnPostRemoveAsync(Guid id)
        {
            try
            {
                var tipo = await _appServiceTipo.GetIdAsync(id);

                await _appServiceTipo.RemoveAsync(tipo);

                await OnLoad();

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar remover tipo!" + "\n" + ex.Message;
            }
        }
    }
}
