using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Cnpj.Interfaces;
using Sim.Domain.Cnpj.Entity;
using Sim.UI.Web.Functions;

namespace Sim.UI.Web.Pages.Empresa.RFB
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceCnpj _appServiceCnpj;

        [TempData]
        public string StatusMessage{ get; set; }

        [BindProperty(SupportsGet = true)]
        public string GetCNPJ{ get; set; }

        public IEnumerable<BaseReceitaFederal> DoList { get; set; }
        public Pagination<BaseReceitaFederal> PaginationEmpresas { get; set; }
        public int RegCount { get; set; }

        public IndexModel(IAppServiceCnpj appServiceCnpj)
        {
            _appServiceCnpj = appServiceCnpj;
        }

        public async Task OnGetAsync(){ 
            await Task.Run(()=>{});
            StatusMessage = "Iniciando RFB!";
        }

        public async Task OnPostAsync(){
            DoList = await _appServiceCnpj.ListAllMatrizFilialAsync(GetCNPJ);
        }
    }
}