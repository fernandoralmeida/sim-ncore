using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.Sebrae.Interfaces;
using Sim.Domain.Sebrae.Model;

namespace Sim.UI.Web.Pages.SebraeAqui.Reports;

[Authorize(Roles = "Administrador,M_Sebrae,M_SebraeAdmin")]
public class IndexModel : PageModel
{
    private readonly IAppServiceEvento _appevento;
    private readonly IAppServiceSebrae _appsebrae;

    [BindProperty(SupportsGet = true)]
    public EReports LReports { get; set; }

    [BindProperty]
    public int InputAno { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    public IndexModel(IAppServiceEvento appevento,
                    IAppServiceSebrae appservicesebrae)  {
        _appevento = appevento; 
        _appsebrae = appservicesebrae;   
    }
    public async Task OnGetAsync()
    {
        StatusMessage = "";
        InputAno = DateTime.Today.Year;
        var _list = await _appevento.DoListAsync(s => s.Data.Value.Year == InputAno);
        LReports = await _appevento.DoBIEventosAsync(_list);
    }

    public async Task OnPostAsync()
    {
        var _list = await _appevento.DoListAsync(s => s.Data.Value.Year == InputAno);
        LReports = await _appevento.DoBIEventosAsync(_list);
    }
}

