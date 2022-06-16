using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Sim.UI.Web.Pages.Empresa.Consultas
{
    using Sim.Domain.SDE.Entity;
    using Sim.Application.SDE.Interface;
    

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
                if (ModelState.IsValid)
                {
                    var emp = Task.Run(() => _empresaApp.ConsultaByRazaoSocial(Input.RazaoSocial));

                    await emp;

                    Input = new InputModel
                    {
                        ListaEmpresas = emp.Result
                    };
                }

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }

            return Page();
        }
    }
}
