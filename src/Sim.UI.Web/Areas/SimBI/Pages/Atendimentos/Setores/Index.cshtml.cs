using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using Sim.Application.Indicadores.VModel;
using Sim.Application.Indicadores.Interfaces;

namespace Sim.UI.Web.Areas.SimBI.Pages.Bpp;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IAppServiceAtendimento _appatendimento;
    private readonly IAppIndicadores _indicadores;

    [BindProperty(SupportsGet = true)]
    public VmRAtendimentos LReports { get; set; }
    public int Ano { get; set; }
    public string PageTitle { get; set; }
    public (string Ano, string Active) NavBar { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    public IndexModel(IAppServiceAtendimento appatendimento,
                    IAppIndicadores indicadores)
    {
        _appatendimento = appatendimento;
        _indicadores = indicadores;
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

        IEnumerable<EAtendimento> _list_at;

        if (m == "SEDEMPI")
            _list_at = await _appatendimento.DoListAsync(s => s.Data.Value.Year == Ano);

        else
            _list_at = await _appatendimento.DoListAsync(s => s.Data.Value.Year == Ano && s.Setor == m);


        LReports = await _indicadores.DoAtendimentosAsync(_list_at);
    }
}

