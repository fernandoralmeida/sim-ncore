using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.VM;
using Sim.Domain.Organizacao.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Unidade;

[Authorize(Roles = $"{Areas.Admin.Pages.Admin.Global},{Areas.Admin.Pages.Admin.Settings}")]
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
    
    private async Task OnLoad() {
        var _list = await _appSecretaria.ListAllAsync();
        Unidades = await _appSecretaria.DoListHierarquia1Async(_list);
        var _org = await _appSecretaria.DoListHierarquia0Async(_list);
        Organizacao = _mapper.Map<VMSecretaria>(_org.SingleOrDefault());  
        Hierarquia = new SelectList(Enum.GetNames(typeof(EHierarquia)).Where(x => x == "Secretaria"));
    }

    public async Task OnGetAsync(string id) {
        await OnLoad();
    }

    public async Task<IActionResult> OnPostAsync() {
        try{
            Input.Ativo = true;     
            Input.Dominio = Organizacao.Id;           
            await _appSecretaria.AddAsync(_mapper.Map<EOrganizacao>(Input));
            await OnLoad();                
        }
        catch(Exception ex) {
            StatusMessage = "Erro: " +  ex.Message;
        }
        return Page();  
    }
    
    public async Task OnGetRemove(Guid id) {
        try
        {   
            var _unidade = await _appSecretaria.SingleIdAsync(id);         
            await _appSecretaria.RemoveAsync(_unidade);       
            await OnLoad();     
        }
        catch (Exception ex)
        {
            StatusMessage = "Erro: " + ex.Message;
        }
    }

}

