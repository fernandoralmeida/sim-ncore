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

        public async Task< IActionResult> OnGetAsync()
        {
            Input.ListaPessoas = await _pessoaApp.ListTop10Async();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if(!string.IsNullOrEmpty(Input.CPF))
                    if (Validate.IsCpf(Input.CPF))
                        CpfValido = true;
                                    
                Input.ListaPessoas = await _pessoaApp.ConsultaCPFAsync(Input.CPF);     
                if (!Input.ListaPessoas.Any())
                    StatusMessage = $"Alerta: CPF {Input.CPF} não cadastrado!";
                
                if(!CpfValido)
                    StatusMessage = $"Erro: CNP {Input.CPF} inválido!";
                
                if(Input.CPF.Any())
                    Input.RouteCPF = Input.CPF.MaskRemove();

            }
            catch(Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
            
            return Page();          
        }

    }
}

