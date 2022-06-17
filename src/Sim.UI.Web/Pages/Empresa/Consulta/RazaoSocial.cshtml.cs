using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Sim.Domain.Entity;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Pages.Empresa.Consultas
{
    [Authorize]
    public class RazaoSocialModel : PageModel
    {
        private readonly IAppServiceEmpresa _empresaApp;

        public RazaoSocialModel(IAppServiceEmpresa appServiceEmpresa)
        {
            _empresaApp = appServiceEmpresa;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {


            [DisplayName("CNPJ")]
            public string CNPJ { get; set; }

            [Required]
            [DisplayName("Razao Social")]
            public string RazaoSocial { get; set; }

            public IEnumerable<Empresas> ListaEmpresas { get; set; }

        }

        private async Task LoadAsync()
        {
            var t = Task.Run(() => { });

            await t;

            Input = new InputModel
            {
                ListaEmpresas = new List<Empresas>().ToList()
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                
              Input.ListaEmpresas = await _empresaApp.ConsultaRazaoSocialAsync(Input.RazaoSocial);

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }

            return Page();
        }
    }
}
