using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.VM;
using Sim.Domain.Organizacao.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Unidade.Manage;
public class IndexModel : PageModel
{
    private readonly IAppServiceSecretaria _appSecretaria;
    private readonly IMapper _mapper;
    public IndexModel(IAppServiceSecretaria appSecretaria,
        IMapper mapper)
    {
        _appSecretaria = appSecretaria;
        _mapper = mapper;
    }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public VMSecretaria Input { get; set; }

    [BindProperty]
    public VMSecretaria Organizacao { get; set; }

    public SelectList Hierarquia { get; set; }

    private void OnLoad() {
        
        Hierarquia = new SelectList(Enum.GetNames(typeof(EHierarquia)));
        ViewData["PageTitle"] = "Alterar informações da Unidade";    
    }

    public async Task OnGetAsync(string id)
    {
       OnLoad();  
       Input = _mapper.Map<VMSecretaria>(await _appSecretaria.SingleIdAsync(new Guid(id)));  
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {                
            await _appSecretaria.UpdateAsync(_mapper.Map<EOrganizacao>(Input));
            OnLoad();
            RedirectToPage("/Common/Unidade/Index", new { id = Input.Id } );
        }
        return Page();
    }
}