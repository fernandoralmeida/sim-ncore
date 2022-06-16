using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Sim.UI.Web.Pages.Pat.Add
{
    using Sim.Application.SDE.Interface;
    using Sim.Domain.SDE.Entity;
    using Functions;   

    [Authorize(Roles = "Administrador,M_Pat")]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IAppServiceEmpregos _appServiceEmpregos;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public IndexModel(IAppServiceEmpresa appServiceEmpresa,
            IAppServiceEmpregos appServiceEmpregos) {
        
            _appServiceEmpresa = appServiceEmpresa;
            _appServiceEmpregos = appServiceEmpregos;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Input = new();
            Input.Data = DateTime.Now;            
            var t = Task.Run(() => _appServiceEmpresa.GetById(id));
            await t;
            Input.Empresa = t.Result;
            Input.Salario = "0,00";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            { return Page(); }

            var t = Task.Run(() =>
            {
                var empresa = _appServiceEmpresa.GetById(Input.Empresa.Id);

                var emprego = new Empregos()
                {
                    Empresa = empresa,
                    Data = Input.Data,
                    Experiencia = Input.Experiencia,
                    Vagas = Input.Vagas,
                    Ocupacao = Input.Ocupacao,
                    Pagamento = Input.Pagamento,
                    Salario = Convert.ToDecimal(Input.Salario),
                    Status = Input.Status
                };

                _appServiceEmpregos.Add(emprego);

            } );
            await t;

            return RedirectToPage("/Pat/Index");
        }
    }
}
