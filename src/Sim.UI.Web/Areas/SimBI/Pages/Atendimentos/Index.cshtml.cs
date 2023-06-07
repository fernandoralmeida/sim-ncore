using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Areas.SimBI.Pages.Atendimentos
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceBIAtendimento _biantendimento;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Ano { get; set; }
        public EChartDual Panorama { get; set; }
        public IEnumerable<EChart> Clientes { get; set; }
        public IEnumerable<EChartThree> ClientesMonths { get; set; }
        public IEnumerable<EChart> Servicos { get; set; }
        public IEnumerable<EChartDual> Users { get; set; }
        public (string Ano, string Active) NavBar { get; set; }

        public IndexModel(IAppServiceBIAtendimento appBiAtendimento)
        {
            _biantendimento = appBiAtendimento;

        }
        public async Task OnGetAsync(string ano = null, string m = null)
        {
            if (ano == null)
                Ano = DateTime.Now.Year.ToString();
            else
                Ano = ano;

            if (m == null)
                m = "SEDEMPI";

            NavBar = (Ano, m);
            Panorama = await _biantendimento.DoAsync(Convert.ToInt32(Ano));
            Users = await _biantendimento.DoListUserAsync(Convert.ToInt32(Ano));
        }

        public async Task<JsonResult> OnGetClientesAsync(int ano)
        {
            return new JsonResult(await _biantendimento.DoListClientesAsync(ano));
        }

        public async Task<JsonResult> OnGetSetoresAsync(int ano)
        {
            return new JsonResult(await _biantendimento.DoListSetorAsync(ano));
        }

        public async Task<JsonResult> OnGetSetoresPercentAsync(int ano)
        {
            return new JsonResult(await _biantendimento.DoListSetorPercentAsync(ano));
        }

        public async Task<JsonResult> OnGetCanalAsync(int ano)
        {
            return new JsonResult(await _biantendimento.DoListCanalAsync(ano));
        }

        public async Task<JsonResult> OnGetCanalPercentAsync(int ano)
        {
            return new JsonResult(await _biantendimento.DoListCanalPercentAsync(ano));
        }

        public async Task<JsonResult> OnGetServicosAsync(int ano)
        {
            return new JsonResult(await _biantendimento.DoListServiceAsync(ano));
        }
        public async Task<JsonResult> OnGetAnualAsync(int ano)
        {
            return new JsonResult(await _biantendimento.DoListMonthAsync(ano));
        }

    }
}
