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

    [BindProperty(SupportsGet = true)]
    public VMSecretaria Input { get; set; }

    [BindProperty]
    public VMSecretaria Organizacao { get; set; }
    public IEnumerable<EOrganizacao> Unidades { get; set; }
    
    private async Task OnLoad(Guid id)
    {
        Unidades = await _appSecretaria.ListAllAsync();
        Organizacao = _mapper.Map<VMSecretaria>(await _appSecretaria.SingleIdAsync(id));  
    }

    public async Task OnGetAsync(string id)
    {
        if (!string.IsNullOrEmpty(id)) 
            await OnLoad(new Guid(id));      
    }

    public async Task OnPostAsync()
    {
        try{
            var _unidade = await _appSecretaria.SingleIdAsync(Organizacao.Id);
            _unidade.Ativo = true;
            if (ModelState.IsValid)
                await _appSecretaria.AddAsync(_unidade);  

            await OnLoad(_unidade.Id);
        }
        catch (Exception ex){
            StatusMessage = "Erro: ao tentar incluir Unidade!" + "\n" + ex.Message;
        }      
    }
}

