using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Domain.Entity;
using Sim.Application.Interfaces;
using Sim.UI.Web.Functions;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;

namespace Sim.UI.Web.Pages.Empresa
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpresa  _empresaApp;

        public IndexModel(IAppServiceEmpresa appServiceEmpresa)
        {
            _empresaApp = appServiceEmpresa;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public SelectList Municipios { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Src { get; set; }

        public Pagination<Empresas> PaginationEmpresas { get; set; }
        public int RegCount { get; set; }
        public async Task OnGetAsync(string? Src, int? pag)
        {
            try
            {
                if (pag == null)
                    pag = 1;

                var _list = await _empresaApp.DoListAsyncBy(Src);
                
                RegCount = _list.Count();
                
                var pagesize = 10;

                var _empresas = _list.AsQueryable();       

                PaginationEmpresas = Pagination<Empresas>.Create(_empresas.AsNoTracking(), pag?? 1, pagesize);
            }
            catch(Exception ex)
            {
                StatusMessage ="Erro: " + ex.Message;
            }
        }
    }
}
