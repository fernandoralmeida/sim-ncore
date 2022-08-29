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

        public EChartDual Atendimentos { get; set; }
        public IEnumerable<EChart> Clientes { get; set; }
        public IEnumerable<EChartThree> ClientesMonths { get; set; }
        public IEnumerable<EChart> Servicos { get; set; }

        public IndexModel(IAppServiceBIAtendimento appBiAtendimento) {
            _biantendimento = appBiAtendimento;
        }
        public async Task OnGetAsync() {
            Ano = DateTime.Now.Year.ToString();
            Atendimentos = await _biantendimento.DoAsync(Convert.ToInt32(Ano));
            Servicos = await _biantendimento.DoListServiceAsync(Convert.ToInt32(Ano));
        }

        public async Task<JsonResult> OnGetClientesAsync(int ano) {
            return new JsonResult(await _biantendimento.DoListClientesAsync(ano));        
        }

        public async Task<JsonResult> OnGetClientesMesesAsync(int ano) {
            return new JsonResult(await _biantendimento.DoListClientesAsync(ano));        
        }

        public async Task OnPostAsync() {
            if(Ano.All(char.IsDigit)) {
                if(Convert.ToInt32(Ano) > 0) {
                    Atendimentos = await _biantendimento.DoAsync(Convert.ToInt32(Ano));
                    Servicos = await _biantendimento.DoListServiceAsync(Convert.ToInt32(Ano));
                }
            }
        }

    }
}
