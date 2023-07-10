using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim.Application.Interfaces;
using Sim.Application.Cnpj.Interfaces;

namespace Sim.UI.Web.Areas.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1")]
public class CEmpresas : ControllerBase
{
    private readonly IAppServiceEmpresa _empresa;
    private readonly IAppServiceCnpj _cnpj;
    public CEmpresas(IAppServiceEmpresa empresa,
        IAppServiceCnpj cnpj)
    {
        _empresa = empresa;
        _cnpj = cnpj;
    }

    [HttpGet("empresa/{cnpj}")]
    public async Task<IActionResult> GetEmpresa([FromRoute] string cnpj)
        => Ok(await _empresa.ConsultaCNPJAsync(cnpj));

    [HttpGet("empresas-logradouro/{municipio}/{logradouro}")]
    public async Task<IActionResult> GetEmpresas_logradouro([FromRoute] string municipio, [FromRoute] string logradouro)
        => Ok(await _cnpj.DoListByLogradouroAsync(logradouro, municipio, "Ativa"));

    [HttpGet("empresas-logradouro-map/{municipio}/{logradouro}")]
    public async Task<IActionResult> GetEmpresas_map_logradouro([FromRoute] string municipio, [FromRoute] string logradouro)
        => Ok(await _cnpj.DoMappingLogradourosAsync(logradouro, municipio, "02"));

    [HttpGet("empresas-zona-map/{municipio}/{zona}")]
    public async Task<IActionResult> GetEmpresas_map_zona([FromRoute] string municipio, [FromRoute] string zona)
        => Ok(await _cnpj.DoMappingLogradourosByZonaAsync(zona, municipio, "02"));
}