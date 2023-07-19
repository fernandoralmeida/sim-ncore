using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.Sebrae.Interfaces;
using Sim.Domain.Evento.Model;
using Sim.Domain.Sebrae.Model;

namespace Sim.UI.Web.Pages.Pat.Reports;

[Authorize(Roles = $"{Web.Areas.Admin.Pages.Admin.Global},SEDEMPI Pat")]
public class IndexModel : PageModel {
    
    private readonly IAppServiceEvento _appevento;
    private readonly IAppServiceAtendimento _appatendimento;
    private readonly IAppServiceSebrae _appsebrae;

    [BindProperty(SupportsGet = true)]
    public EReports LReports { get; set; }

    [BindProperty]
    public int InputAno { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    public IndexModel(IAppServiceEvento appevento,
                    IAppServiceAtendimento appatendimento,
                    IAppServiceSebrae appservicesebrae)  {
        _appevento = appevento; 
        _appatendimento = appatendimento;
        _appsebrae = appservicesebrae;   
    }
    public async Task OnGetAsync() {
        StatusMessage = "";
        InputAno = DateTime.Today.Year;
        var _list_ev = await _appevento.DoListAsync(s => s.Data.Value.Year == InputAno && s.Owner == "PAT" && s.Situacao != EEvento.ESituacao.Cancelado);
        var _list_at = await _appatendimento.DoListAsync(s => s.Data.Value.Year == InputAno && s.Setor == "PAT");
        LReports = await _appsebrae.DoReportAsync(_list_at, _list_ev);
    }

    public async Task OnPostAsync() {
        var _list_ev = await _appevento.DoListAsync(s => s.Data.Value.Year == InputAno && s.Owner == "PAT" && s.Situacao != EEvento.ESituacao.Cancelado);
        var _list_at = await _appatendimento.DoListAsync(s => s.Data.Value.Year == InputAno && s.Setor == "PAT");
        LReports = await _appsebrae.DoReportAsync(_list_at, _list_ev);
    }
}
