using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.Sebrae.Interfaces;
using Sim.Domain.Evento.Model;
using Sim.Domain.Sebrae.Model;

namespace Sim.UI.Web.Areas.SimBI.Pages.Bpp;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IAppServiceEvento _appevento;
    private readonly IAppServiceAtendimento _appatendimento;
    private readonly IAppServiceSebrae _appsebrae;

    [BindProperty(SupportsGet = true)]
    public EReports LReports { get; set; }
    public int Ano { get; set; }
    public string PageTitle { get; set; }
    public (string Ano, string Active) NavBar { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    public IndexModel(IAppServiceEvento appevento,
                    IAppServiceAtendimento appatendimento,
                    IAppServiceSebrae appservicesebrae)
    {
        _appevento = appevento;
        _appatendimento = appatendimento;
        _appsebrae = appservicesebrae;
    }
    public async Task OnGetAsync(string ano = null, string m = null)
    {
        StatusMessage = "";
        PageTitle = m;
        if (ano == null)
            Ano = DateTime.Now.Year;
        else
            Ano = Convert.ToInt32(ano);

        if (m == null)
            m = "SEDEMPI";

        NavBar = (Ano.ToString(), m);

        var _list_ev = await _appevento.DoListAsync(s => s.Data.Value.Year == Ano && s.Owner == m && s.Situacao != EEvento.ESituacao.Cancelado);
        var _list_at = await _appatendimento.DoListAsync(s => s.Data.Value.Year == Ano && s.Setor == m);
        LReports = await _appsebrae.DoReportAsync(_list_at, _list_ev);
    }
}

