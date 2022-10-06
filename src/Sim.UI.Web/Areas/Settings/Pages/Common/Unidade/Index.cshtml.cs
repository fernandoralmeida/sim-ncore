using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.VM;
using Sim.Domain.Organizacao.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Unidade;
public class IndexModel : PageModel
{
    //private readonly IAppServicePrefeitura _appServicePrefeitura;
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
    public IEnumerable<EOrganizacao> Unidades { get; set; }
    public SelectList Hierarquia { get; set; }
    
    private async Task OnLoad(Guid id)
    {
        Unidades = await _appSecretaria.DoListHierarquia1Async(await _appSecretaria.ListAllAsync());
        Organizacao = _mapper.Map<VMSecretaria>(await _appSecretaria.SingleIdAsync(id));  
        Hierarquia = new SelectList(Enum.GetNames(typeof(EHierarquia)).Where(x => x != "Matriz"));
    }

    public async Task OnGetAsync(string id)
    {
        if (!string.IsNullOrEmpty(id)) 
            await OnLoad(new Guid(id));      

        ViewData["PageTitle"] = "Inclua e gerêncie as unidades da organização";
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try{
            Input.Ativo = true;     
            Input.Dominio = Organizacao.Id;           
            await _appSecretaria.AddAsync(_mapper.Map<EOrganizacao>(Input));
            await OnLoad(Input.Id);                
        }
        catch(Exception ex) {
            StatusMessage = "Erro: " +  ex.Message;
        }

        return Page();  
    }
}

