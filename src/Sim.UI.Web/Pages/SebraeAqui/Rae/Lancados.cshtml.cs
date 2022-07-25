using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Application.Interfaces;
using Sim.UI.Web.Functions;
using Microsoft.EntityFrameworkCore;

namespace Sim.UI.Web.Pages.SebraeAqui.Rae
{

    [Authorize(Roles = "Administrador,M_Sebrae")]
    public class LancadosModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        public Pagination<Domain.Entity.Atendimento> PaginationAtendimentos { get; set; }
        public LancadosModel(IAppServiceAtendimento appServiceAtendimento)
        {
            _appServiceAtendimento = appServiceAtendimento;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public int RegCount { get; set; }

        public async Task OnGetAsync(int? pag)
        {
            var _list = await _appServiceAtendimento.ListRaeLancadosAsync(User.Identity.Name);

            RegCount = _list.Count();

            IQueryable<Domain.Entity.Atendimento> _atendimentos = _list.AsQueryable();

            if (pag == null)
                pag = 1;

            var pagesize = 10;

            PaginationAtendimentos = Pagination<Domain.Entity.Atendimento>.Create(_atendimentos.AsNoTracking(), pag ?? 1, pagesize);

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
