using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

using Sim.Domain.Entity;
using Sim.Application.Interfaces;
using Sim.Identity.Interfaces;
using Sim.Identity.Entity;
using OfficeOpenXml;

namespace Sim.UI.Web.Pages.Atendimento.Consulta
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceServico _appServiceServico;
        private readonly IServiceUser _appIdentity;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Src { get; set; }

        public IEnumerable<EAtendimento> ListaAtendimento { get; set; }

        public IndexModel(IAppServiceAtendimento appServiceAtendimento,
            IServiceUser appServiceUser,
            IAppServiceServico appServiceServico)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appIdentity = appServiceUser;
            _appServiceServico = appServiceServico;
        }

        public async Task OnGetAsync()
        {
            var lista = await _appServiceAtendimento.DoListAendimentosAsyncBy(Src);
            ListaAtendimento = lista.ToList();
        }
        public async Task OnPostAsync()
        {
            try
            {
                var lista = await _appServiceAtendimento.DoListAendimentosAsyncBy(Src);
                ListaAtendimento = lista.ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                ListaAtendimento = new List<EAtendimento>();
            }
        }
    }
}
