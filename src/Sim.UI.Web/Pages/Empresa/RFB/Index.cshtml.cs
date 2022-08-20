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
        public string Search{ get; set; }

        public Pagination<BaseReceitaFederal> Pagination { get; set; }
        public int RegCount { get; set; }

        public IndexModel(IAppServiceCnpj appServiceCnpj)
        {
            _appServiceCnpj = appServiceCnpj;
        }

        private async Task<Pagination<BaseReceitaFederal>> DoListAsync(string s, int? p)
        {
            var list = await _appServiceCnpj.DoListBaseRazaoSociosAsync(s);
            
            RegCount = list.Count();
            var pagesize = 10;
            var _empresas = list.AsQueryable();
            return Pagination<BaseReceitaFederal>.Create(_empresas.AsNoTracking(), p?? 1, pagesize);
        }

        public async Task OnGetAsync(string s, int? p)
        { 
            try
            {
                Search = s;
                Pagination = await DoListAsync(s, p);
            }
            catch(Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
        }
    }
}