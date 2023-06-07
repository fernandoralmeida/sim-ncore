using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Application.Interfaces;
using Sim.UI.Web.Functions;
using Microsoft.EntityFrameworkCore;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.SebraeAqui
{
    [Authorize(Roles = $"{Web.Areas.Admin.Pages.Admin.Global},SEDEMPI Sebrae Aqui")]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;

        public Pagination<EAtendimento> PaginationAtendimentos { get; set; }
        public IndexModel(IAppServiceAtendimento appServiceAtendimento)
        {
            _appServiceAtendimento = appServiceAtendimento;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public int RegCount { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int Src { get; set; }
               
        public async Task OnGetAsync(int Src, int? pag)
        {
            if(Src == 0 )
                Src = DateTime.Now.Year;

            var _list = await _appServiceAtendimento.ListRaeNaoLancadosAsync(User.Identity.Name, Src);

            RegCount = _list.Count();

            IQueryable<EAtendimento> _atendimentos = _list.OrderBy(o => o.Data).AsQueryable();

            if (pag == null)
                pag = 1;

            var pagesize = 10;

            PaginationAtendimentos = Pagination<EAtendimento>.Create(_atendimentos.AsNoTracking(), pag ?? 1, pagesize);

            if (!PaginationAtendimentos.Any())
            {
                StatusMessage = string.Format("Não há atendimentos para lançar");
            }
        }

        public JsonResult OnGetPreview(string id)
        {
            return new JsonResult(_appServiceAtendimento.GetAtendimentoAsync(new Guid(id)).Result);
        }
    }
}
