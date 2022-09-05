using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.Pat
{
    [Authorize(Roles = "Administrador,M_Pat")]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpregos _appEmpregos;
        private readonly IAppServicePessoa _appPessoa;
        private readonly IAppServiceEmpresa _appEmpresa;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public string InputSearch { get; set; }

        [BindProperty]
        public string CNPJ { get; set; }
        public IEnumerable<Empregos> ListaEmpregos { get; set; }
        public IEnumerable<Empresas> ListaEmpresas { get; set; }

        public IndexModel(IAppServiceEmpregos appServiceEmpregos,
            IAppServiceEmpresa appServiceEmpresa,
            IAppServicePessoa appServicePessoa)
        {
            _appEmpregos = appServiceEmpregos;
            _appPessoa = appServicePessoa;
            _appEmpresa = appServiceEmpresa;
        }
        public async Task OnGetAsync()
        {
            ListaEmpregos = await _appEmpregos.DoListEmpregosAsync();
        }

        public async Task OnPostAsync(){
            ListaEmpregos = await _appEmpregos.DoListEmpregosAsyncBy(InputSearch);
        }

        public async Task<JsonResult> OnGetClienteAsync(string id){                        
            return await Task.Run(async () => {                    
                var _emp = await _appEmpresa.SingleIdAsync(new Guid(id));
                if(_emp != null)
                    return new JsonResult(_emp);
                else
                    return new JsonResult(await _appPessoa.SingleIdAsync(new Guid(id)));       
            });                
        }
    }
}
