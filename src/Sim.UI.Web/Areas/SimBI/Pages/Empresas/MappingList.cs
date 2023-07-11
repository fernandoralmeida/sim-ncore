using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Cnpj.Interfaces;
using Sim.Domain.Cnpj.Entity;

namespace Sim.UI.Web.Areas.SimBI.Pages.Empresas;

[Authorize]
public class MappingListaModel : PageModel
{
    private readonly IAppServiceCnpj _appEmpresa;

    [BindProperty(SupportsGet = true)]
    public IEnumerable<BaseReceitaFederal> ListEmpresas { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    public MappingListaModel(IAppServiceCnpj appEmpresa)
    {
        _appEmpresa = appEmpresa;
        ListEmpresas = new List<BaseReceitaFederal>();
    }

    public async Task OnGetAsync(string l, string m, string? z)
    {
        StatusMessage = "";

        if (string.IsNullOrEmpty(z))

            ListEmpresas = await _appEmpresa.DoListAsync(
                    s => s.Logradouro.Contains(l) &&
                    s.Municipio == m &&
                    s.SituacaoCadastral == "02");
        else
            ListEmpresas = await _appEmpresa.DoListAsync(
                    s => s.Logradouro.Contains(l) &&
                    s.Bairro.Contains(z) &&
                    s.Municipio == m &&
                    s.SituacaoCadastral == "02");

    }
}

