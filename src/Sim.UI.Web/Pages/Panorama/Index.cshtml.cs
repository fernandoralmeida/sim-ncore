using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Sim.Identity.Entity;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.Panorama;
public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAppServiceAtendimento _appAtendimento;
    private readonly IAppServiceStatusAtendimento _appServiceStatusAtendimento;
    private readonly IAppServiceBIAtendimento _biantendimento;

    public EChartDual Panorama { get; set; }

    public class InputSetor {
        public string Setor { get; set; }
        public IEnumerable<InputUser> Lista { get; set; }
    }    
    public class InputUser {
        public string Atendente { get; set; }
        public string Status { get; set; }
    }

    [TempData]
    public string StatusMessage { get; set; }

    public IndexModel(UserManager<ApplicationUser> userManager,
        IAppServiceAtendimento appAtendimento,
        IAppServiceStatusAtendimento appServiceStatusAtendimento,
        IAppServiceBIAtendimento appServiceBIAtendimento)
    {
        _userManager = userManager;
        _appAtendimento = appAtendimento;
        _appServiceStatusAtendimento = appServiceStatusAtendimento;
        _biantendimento = appServiceBIAtendimento;
    }

    private async Task<IEnumerable<InputSetor>> DoListUsersAsync() {
        return await Task.Run(async () => { 
            var _list = new List<InputSetor>();

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
    
    public void OnGet()
    {   }

    public async Task<JsonResult> OnGetAtendimentosAsync() {
        return new JsonResult(await _biantendimento.DoAsync(DateTime.Now.Year));        
    }
    public async Task<JsonResult> OnGetUsersAsync() {
        return new JsonResult(await DoListUsersAsync());        
    }

    public async Task<JsonResult> OnGetClientesAsync() {
        return new JsonResult(await _biantendimento.DoListClientesAsync(DateTime.Now.Year));        
    }

    public async Task<JsonResult> OnGetSetoresAsync() {
        return new JsonResult(await _biantendimento.DoListSetorAsync(DateTime.Now.Year));        
    }

    public async Task<JsonResult> OnGetSetoresPercentAsync() {
        return new JsonResult(await _biantendimento.DoListSetorPercentAsync(DateTime.Now.Year));        
    }

    public async Task<JsonResult> OnGetCanalAsync() {
        return new JsonResult(await _biantendimento.DoListCanalAsync(DateTime.Now.Year));        
    }

    public async Task<JsonResult> OnGetCanalPercentAsync() {
        return new JsonResult(await _biantendimento.DoListCanalPercentAsync(DateTime.Now.Year));        
    }

    public async Task<JsonResult> OnGetServicosAsync() {
        return new JsonResult(await _biantendimento.DoListServiceAsync(DateTime.Now.Year));        
    }
    public async Task<JsonResult> OnGetAnualAsync() {
        return new JsonResult(await _biantendimento.DoListMonthAsync(DateTime.Now.Year));        
    }
}

