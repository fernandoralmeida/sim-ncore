using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        public string src{ get; set; }

        public Pagination<BaseReceitaFederal> Pagination { get; set; }
        public int RegCount { get; set; }

        public IndexModel(IAppServiceCnpj appServiceCnpj)
        {
            _appServiceCnpj = appServiceCnpj;
        }

        public async Task OnGetAsync(string src, int? p)
        { 
            try
            {
                if (p == null)
                    p = 1;

                var _list = await _appServiceCnpj.DoListBaseRazaoSociosAsync(src);
                
                RegCount = _list.Count();
                
                var pagesize = 10;

                var _empresas = _list.AsQueryable();       

                Pagination = Pagination<BaseReceitaFederal>.Create(_empresas.AsNoTracking(), p?? 1, pagesize);
            }
            catch(Exception ex)
            {
                StatusMessage ="Erro: " + ex.Message;
            }
        }
    }
}