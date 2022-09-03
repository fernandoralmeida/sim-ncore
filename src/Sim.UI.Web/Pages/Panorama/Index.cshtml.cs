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
    public IEnumerable<EChart> Clientes { get; set; }
    public IEnumerable<EChartThree> ClientesMonths { get; set; }
    public IEnumerable<EChart> Servicos { get; set; }
    public IEnumerable<EChartDual> Users { get; set; }

    [BindProperty(SupportsGet = true)]
    public InputModelIndex Input { get; set; }

    [BindProperty(SupportsGet = true)]
    public IEnumerable<InputModelIndex> ListaPAT { get; set; }

    [BindProperty(SupportsGet = true)]
    public IEnumerable<InputModelIndex> ListaBPP { get; set; }

    [BindProperty(SupportsGet = true)]
    public IEnumerable<InputModelIndex> ListaSA { get; set; }

    [BindProperty(SupportsGet = true)]
    public IEnumerable<InputModelIndex> ListaSE { get; set; }

    public class InputModelIndex
    {
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

    private async Task<IEnumerable<InputModelIndex>> ListUsersAsync(string setor)
    {
        var list = new List<InputModelIndex>();
        var users = await _userManager.GetUsersInRoleAsync(setor);

        foreach (ApplicationUser s in users)
        {
            var t = await _appServiceStatusAtendimento.ListUserAsync(s.UserName);

            if(t.Any())
                if (t.FirstOrDefault().Online)
                {
                    var ativo = await _appAtendimento.ListAtendimentoAtivoAsync(s.UserName);

                    if (ativo.Any())
                        list.Add(new InputModelIndex() { Atendente = s.Name, Status = "Em Atendimento" });

                    else
                        list.Add(new InputModelIndex() { Atendente = s.Name, Status = "Disponível" });
                }
        }

        return list;
    }
    
    public async Task OnGet()
    {
        ListaPAT = await ListUsersAsync("M_Pat");
        ListaBPP = await ListUsersAsync("M_BancoPovo");
        ListaSA = await ListUsersAsync("M_Sebrae");
        ListaSE = await ListUsersAsync("M_SalaEmpreendedor");
        Panorama = await _biantendimento.DoAsync(DateTime.Now.Year);
        Users = await _biantendimento.DoListUserAsync(DateTime.Now.Year);
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

