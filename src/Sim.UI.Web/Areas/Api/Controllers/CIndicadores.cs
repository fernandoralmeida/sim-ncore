using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim.Application.Interfaces;
using Sim.Application.BancoPovo.Interfaces;
using Sim.Domain.Entity;
using Sim.Application.Indicadores.Interfaces;

namespace Sim.UI.Web.Areas.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1")]
public class CIndicadores : ControllerBase
{
    private readonly IAppServiceAtendimento _atendimentos;
    private readonly IAppIndicadores _indicadores;
    private readonly IAppServiceBIEmpregos _empregos;
    private readonly IAppServiceContratos _contratos_bpp;
    private readonly IAppServiceEvento _eventos;

    public CIndicadores(IAppServiceAtendimento atendimento,
        IAppIndicadores indicadores,
        IAppServiceBIEmpregos empregos,
        IAppServiceContratos contratos,
        IAppServiceEvento eventos)
    {
        _atendimentos = atendimento;
        _indicadores = indicadores;
        _empregos = empregos;
        _contratos_bpp = contratos;
        _eventos = eventos;
    }

    [HttpGet("indicadores/antendimentos/{ano?}/{setor?}")]
    public async Task<IActionResult> DoAtendimentos([FromRoute] int ano, [FromRoute] string setor)
     => Ok(setor == null ?
            await _indicadores.DoAtendimentosAsync(s => s.Data.Value.Year == ano) :
            await _indicadores.DoAtendimentosAsync(s => s.Data.Value.Year == ano && s.Setor == setor));


    [HttpGet("indicadores/empregos/{ano}")]
    public async Task<IActionResult> DoEmpregos([FromRoute] string ano)
        => Ok(await _empregos.DoEmpregosAtivos(Convert.ToInt32(ano)));

    [HttpGet("indicadores/contratos/{ano}")]
    public async Task<IActionResult> DoContratos([FromRoute] string ano)
        => Ok(await _contratos_bpp.DoReportsAsync(await _contratos_bpp.DoListAsync(s => s.Data.Value.Year == Convert.ToInt32(ano))));

}