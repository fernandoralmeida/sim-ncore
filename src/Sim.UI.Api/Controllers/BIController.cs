using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Sim.Identity.Entity;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Api.Controllers;

[ApiController]
[Route("v1")]
public class BIController : ControllerBase {
    private readonly ILogger<BIController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAppServiceAtendimento _appAtendimento;
    private readonly IAppServiceStatusAtendimento _appServiceStatusAtendimento;
    private readonly IAppServiceBIAtendimento _biantendimento;
    private readonly IAppServiceSetor _appSetores;

    private class InputSetor {
        public string? Setor { get; set; }
        public IEnumerable<InputUser>? Lista { get; set; }
    }    
    private class InputUser {
        public string? Atendente { get; set; }
        public string? Status { get; set; }
    }

    public BIController (ILogger<BIController> logger,
        UserManager<ApplicationUser> userManager,
        IAppServiceAtendimento appServiceAtendimento,
        IAppServiceStatusAtendimento appServiceStatusAtendimento,
        IAppServiceBIAtendimento appServiceBIAtendimento,
        IAppServiceSetor appServiceSetor) {
        _logger = logger;
        _userManager = userManager;
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

    [HttpGet]
    [Route("bi-users_status")]
    public async Task<IActionResult> GetUsers() {
        return Ok(await DoListUsersAsync());
    }  

    [HttpGet]
    [Route("bi-atendimentos")]
    public async Task<IActionResult> OnGetAtendimentosAsync() {
        return Ok(await _biantendimento.DoAsync(DateTime.Now.Year));        
    }

    [HttpGet]
    [Route("bi-at-clients")]
    public async Task<IActionResult> OnGetClientesAsync() {
        return Ok(await _biantendimento.DoListClientesAsync(DateTime.Now.Year));        
    }

    [HttpGet]
    [Route("bi-at-setores")]
    public async Task<IActionResult> OnGetSetoresAsync() {
        return Ok(await _biantendimento.DoListSetorAsync(DateTime.Now.Year));        
    }

    [HttpGet]
    [Route("bi-at-setores-t")]
    public async Task<IActionResult> OnGetSetoresPercentAsync() {
        return Ok(await _biantendimento.DoListSetorPercentAsync(DateTime.Now.Year));        
    }

    [HttpGet]
    [Route("bi-at-canal")]
    public async Task<IActionResult> OnGetCanalAsync() {
        return Ok(await _biantendimento.DoListCanalAsync(DateTime.Now.Year));        
    }

    [HttpGet]
    [Route("bi-at-canal-t")]
    public async Task<IActionResult> OnGetCanalPercentAsync() {
        return Ok(await _biantendimento.DoListCanalPercentAsync(DateTime.Now.Year));        
    }

    [HttpGet]
    [Route("bi-at-servicos")]
    public async Task<IActionResult> OnGetServicosAsync() {
        return Ok(await _biantendimento.DoListServiceAsync(DateTime.Now.Year));        
    }

    private async Task<IEnumerable<InputSetor>> DoListUsersAsync() {
        return await Task.Run(async () => { 
            var _list = new List<InputSetor>();
            var _setores = await _appSetores.ListAllAsync();

            foreach(var _roles in new string[]{"M_Pat", "M_BancoPovo", "M_Sebrae", "M_SalaEmpreendedor"}) {

                var _innerlist = new List<InputUser>();
                var users = await _userManager.GetUsersInRoleAsync(_roles);

                foreach (ApplicationUser s in users)
                {
                    var t = await _appServiceStatusAtendimento.ListUserAsync(s.UserName);

                    if(t.Any())
                        if (t.FirstOrDefault().Online)
                        {
                            var ativo = await _appAtendimento.ListAtendimentoAtivoAsync(s.UserName);

                            if (ativo.Any())
                                _innerlist.Add(new InputUser() { Atendente = s.Name, Status = "Em Atendimento"});

                            else
                                _innerlist.Add(new InputUser() { Atendente = s.Name, Status = "Disponível" });
                        }
                }

                _list.Add(new InputSetor(){ Setor = _roles.Replace("M_",""), Lista = _innerlist }) ;
            }
            return _list;
        });
    }

}