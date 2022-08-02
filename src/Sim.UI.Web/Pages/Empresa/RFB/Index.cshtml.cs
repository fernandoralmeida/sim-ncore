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

        [BindProperty(SupportsGet = true)]
        public int? Tipo { get; set; }

        public Pagination<BaseReceitaFederal> Pagination { get; set; }
        public int RegCount { get; set; }

        public IndexModel(IAppServiceCnpj appServiceCnpj)
        {
            _appServiceCnpj = appServiceCnpj;
        }

        private async Task<Pagination<BaseReceitaFederal>> DoListAsync(string c, int? t, int? p)
        {
            IEnumerable<BaseReceitaFederal> list;
            switch(t)
            {
                case 1:
                    list = await _appServiceCnpj.ListAllRazaoSocialAsync(c);
                    break;
                
                case 2:
                    list = await _appServiceCnpj.ListAllSocioAsync(c);
                    StatusMessage = "Erro: " + Tipo;
                    break;

                default:
                    list = await _appServiceCnpj.ListAllMatrizFilialAsync(c);    
                    break;
            }
            RegCount = list.Count();
            var pagesize = 10;
            var _empresas = list.AsQueryable();
            return Pagination<BaseReceitaFederal>.Create(_empresas.AsNoTracking(), p?? 1, pagesize);
        }

        public async Task OnGetAsync(string s, int? t, int? p)
        { 
            try
            {
                if(t == null)
                    Tipo = 0;
                else
                    Tipo = t;                

                Pagination = await DoListAsync(s, t, p);
            }
            catch(Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
        }

        public async Task OnPostAsync(){

            try
            {
                Pagination = await DoListAsync(Search.MaskRemove(), Tipo, 1);
            }
            catch(Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
        }
    }
}