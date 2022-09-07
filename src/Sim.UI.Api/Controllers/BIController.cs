using Microsoft.AspNetCore.Mvc;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Api.Controllers;

[ApiController]
[Route("v1")]
public class BIController : ControllerBase {
    private readonly ILogger<BIController> _logger;
    private readonly IAppServiceAtendimento _appAtendimento;
    private readonly IAppServiceStatusAtendimento _appServiceStatusAtendimento;
    private readonly IAppServiceBIAtendimento _biantendimento;
    private readonly IAppServiceSetor _appSetores;

    public BIController (ILogger<BIController> logger,
        IAppServiceAtendimento appServiceAtendimento,
        IAppServiceStatusAtendimento appServiceStatusAtendimento,
        IAppServiceBIAtendimento appServiceBIAtendimento,
        IAppServiceSetor appServiceSetor) {
        _logger = logger;
        _appAtendimento = appServiceAtendimento;
        _appServiceStatusAtendimento = appServiceStatusAtendimento;
        _biantendimento = appServiceBIAtendimento;
        _appSetores = appServiceSetor;
    }

    [HttpGet]
    [Route("bi-month-atendimentos")]
    public async Task<IActionResult> Get() {
        return Ok(await _biantendimento.DoListMonthAsync(DateTime.Now.Year));
    }

    



}