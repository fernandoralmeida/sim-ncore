using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Indicadores.Interfaces;
using Sim.Application.Indicadores.VModel;
using Sim.Application.Interfaces;
using Sim.Domain.Organizacao.Model;
using Sim.Domain.Helpers;

namespace Sim.UI.Web.Areas.Dashboards.Pages.Indicadores;

[Authorize]
public class AtendimentosPage : PageModel
{
    private readonly IAppIndicadores _indicadores;
    private readonly IAppServiceSecretaria _organizacao;

    public VmRAtendimentos LReports { get; set; }
    public (int Ano, string Setor, string Page, IEnumerable<string> Setores) NavBar { get; set; }

    public string AtendimentosMonth { get; set; }
    public string[] Perfil { get; set; }
    public string[] Canais { get; set; }
    public string ServicosMonth { get; set; }
    public string AtTimeDay { get; set; }
    public string SvTimeDay { get; set; }

    public AtendimentosPage(IAppIndicadores indicadores,
        IAppServiceSecretaria organizacao)
    {
        _indicadores = indicadores;
        _organizacao = organizacao;
    }

    public async Task OnGetAsync(int ano = 0, string setor = null)
    {
        setor ??= "SEDEMPI";
        ano = ano == 0 ? DateTime.Now.Year : ano;
        NavBar = (ano, setor, "Atendimentos", await Setores());
        LReports = setor == "SEDEMPI" ?
            await _indicadores.DoAtendimentosAsync(s => s.Data.Value.Year == ano) :
            await _indicadores.DoAtendimentosAsync(s => s.Data.Value.Year == ano && s.Setor == setor);


        string _atmonth = string.Empty;
        foreach (var m in LReports.AtendimentosMonth)
            _atmonth += string.Format(@"{{x:`{0}`,y:{1}}},", m.Key, m.Value);

        string _svmonth = string.Empty;
        foreach (var m in LReports.ServicesMonth)
            _svmonth += string.Format(@"{{x:`{0}`,y:{1}}},", m.Key, m.Value);

        string[] _perfil = new string[2];
        foreach (var p in LReports.PerfilAtendimento)
        {
            _perfil[0] += string.Format(@"{0},", p.Value);
            _perfil[1] += string.Format(@"`{0}`,", p.Key.NormalizeText());
        }

        string _attimeday = string.Empty;
        foreach(var (timeday, valor) in LReports.TimeDay)
            _attimeday += string.Format(@"{{x:`{0}H`,y:{1}}},", timeday, valor);

        string _svtimeday = string.Empty;
        foreach (var (timeday, valor) in LReports.ServTimeDay)
            _svtimeday += string.Format(@"{{x:`{0}H`,y:{1}}},", timeday, valor);

        string[] _canal = new string[2];
        foreach (var (canal, valor, percent) in LReports.Canais)
        {
            _canal[0] += string.Format(@"{0},", valor);
            _canal[1] += string.Format(@"`{0}`,", canal);
        }

        AtTimeDay = _attimeday.Length > 0 ? _attimeday[..^1] : _attimeday;
        SvTimeDay = _svtimeday.Length > 0 ? _svtimeday[..^1] : _svtimeday;
        Perfil = _perfil;
        Canais = _canal;        
        AtendimentosMonth = _atmonth.Length > 0 ? _atmonth[..^1] : _atmonth;
        ServicosMonth = _svmonth.Length > 0 ? _svmonth[..^1] : _svmonth;
    }

    private async Task<IEnumerable<string>> Setores()
    {
        return from st in await _organizacao.DoList(s => s.Hierarquia >= EHierarquia.Secretaria)
               select st.Acronimo;
    }
}
