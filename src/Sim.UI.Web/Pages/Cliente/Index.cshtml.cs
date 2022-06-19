using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using Sim.Domain.Entity;
using Sim.Application.Interfaces;
using Sim.UI.Web.Functions;

namespace Sim.UI.Web.Pages.Cliente
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServicePessoa _pessoaApp;

        public IndexModel(IAppServicePessoa appServicePessoa)
        {
            _pessoaApp = appServicePessoa;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public bool CpfValido = false;

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }
        public class InputModel
        {                  
            public string CPF { get; set; }
            public string Nome { get; set; }
            public string RouteCPF { get; set; }
            public IEnumerable<Pessoa> ListaPessoas { get; set; }
        }

        private async Task Load()
        {
            Input.ListaPessoas = await _pessoaApp.ListTop10Async();
        }

        public async Task< IActionResult> OnGetAsync()
        {
            await Load();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Input.CPF != null)
                    {
                        Input.RouteCPF = Input.CPF.MaskRemove();

                        if (Validate.IsCpf(Input.CPF))
                        {
                            StatusMessage = "";
                            CpfValido = true;
                        }
                        else
                        {
                            StatusMessage = "Erro: CPF inválido!";
                            CpfValido = false;
                        }

                        Input.ListaPessoas = await _pessoaApp.ConsultaCPFAsync(Input.CPF);

                        if(CpfValido && !Input.ListaPessoas.Any())
                            StatusMessage = "Erro: Pessoa não cadastrada!";
                    }
                    else
                        Input.ListaPessoas = await _pessoaApp.ConsultaNomeAsync(Input.Nome);

                }                
            }
            catch(Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
            
            return Page();          
        }

    }
}

