using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Domain.Organizacao.Model;

namespace Sim.UI.Web.Areas.Dashboards.Pages.Indicadores;

[Authorize]
public class DashContratos : PageModel
{
    private readonly IAppServiceSecretaria _organizacao;
    public (int Ano, string Setor, string Page, IEnumerable<string> Setores) NavBar { get; set; }
    public DashContratos(IAppServiceSecretaria organizacao)
    {
        _organizacao = organizacao;
    }
    public async Task OnGetAsync(int ano = 0, string setor = null)
    {
        setor ??= "6607";
        ano = ano == 0 ? DateTime.Now.Year : ano;
        NavBar = (ano, setor, "Contratos", await Setores());
    }

    private async Task<IEnumerable<string>> Setores()
    {
        return from st in await _organizacao.DoList(s => s.Hierarquia >= EHierarquia.Secretaria)
               select st.Acronimo;
    }
}