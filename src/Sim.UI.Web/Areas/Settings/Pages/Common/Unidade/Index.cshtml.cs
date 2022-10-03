using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.VM;
using Sim.Domain.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Unidade;
public class IndexModel : PageModel
{
    private readonly IAppServicePrefeitura _appServicePrefeitura;
    private readonly IAppServiceSecretaria _appSecretaria;
    private readonly IMapper _mapper;
    public IndexModel(IAppServicePrefeitura appService,
        IAppServiceSecretaria appSecretaria,
        IMapper mapper)
    {
        _appServicePrefeitura = appService;
        _appSecretaria = appSecretaria;
        _mapper = mapper;
    }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    public VMSecretaria Input { get; set; }

    [BindProperty]
    public VMPrefeitura Organizacao { get; set; }
    public IEnumerable<Sim.Domain.Entity.Secretaria> Unidades { get; set; }
    
    private async Task OnLoad(Guid id)
    {
        Unidades = await _appSecretaria.ListAllAsync();
        Organizacao = _mapper.Map<VMPrefeitura>(await _appServicePrefeitura.SingleIdAsync(id));  
    }

    public async Task OnGetAsync(string id)
    {
        if (!string.IsNullOrEmpty(id)) 
            await OnLoad(new Guid(id));      
    }

    public async Task OnPostAsync()
    {
        try{
            var _unidade = _mapper.Map<Sim.Domain.Entity.Secretaria>(Input);
            _unidade.Owner = await _appServicePrefeitura.SingleIdAsync(Organizacao.Id);
            _unidade.Ativo = true;
            if (ModelState.IsValid)
                await _appSecretaria.AddAsync(_unidade);  

            await OnLoad(_unidade.Owner.Id);
        }
        catch (Exception ex){
            StatusMessage = "Erro: ao tentar incluir Unidade!" + "\n" + ex.Message;
        }      
    }
}

