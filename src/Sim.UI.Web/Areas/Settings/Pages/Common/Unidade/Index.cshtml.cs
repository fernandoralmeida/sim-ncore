using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.VM;
<<<<<<< HEAD
using Sim.Domain.Organizacao.Model;
=======
using Sim.Domain.Entity;
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Unidade;
public class IndexModel : PageModel
{
<<<<<<< HEAD
    //private readonly IAppServicePrefeitura _appServicePrefeitura;
    private readonly IAppServiceSecretaria _appSecretaria;
    private readonly IMapper _mapper;
    public IndexModel(IAppServiceSecretaria appSecretaria,
        IMapper mapper)
    {
=======
    private readonly IAppServicePrefeitura _appServicePrefeitura;
    private readonly IAppServiceSecretaria _appSecretaria;
    private readonly IMapper _mapper;
    public IndexModel(IAppServicePrefeitura appService,
        IAppServiceSecretaria appSecretaria,
        IMapper mapper)
    {
        _appServicePrefeitura = appService;
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
        _appSecretaria = appSecretaria;
        _mapper = mapper;
    }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    public VMSecretaria Input { get; set; }

    [BindProperty]
<<<<<<< HEAD
    public VMSecretaria Organizacao { get; set; }
    public IEnumerable<EOrganizacao> Unidades { get; set; }
=======
    public VMPrefeitura Organizacao { get; set; }
    public IEnumerable<Sim.Domain.Entity.Secretaria> Unidades { get; set; }
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
    
    private async Task OnLoad(Guid id)
    {
        Unidades = await _appSecretaria.ListAllAsync();
<<<<<<< HEAD
        Organizacao = _mapper.Map<VMSecretaria>(await _appSecretaria.SingleIdAsync(id));  
=======
        Organizacao = _mapper.Map<VMPrefeitura>(await _appServicePrefeitura.SingleIdAsync(id));  
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
    }

    public async Task OnGetAsync(string id)
    {
        if (!string.IsNullOrEmpty(id)) 
            await OnLoad(new Guid(id));      
    }

    public async Task OnPostAsync()
    {
        try{
<<<<<<< HEAD
            var _unidade = await _appSecretaria.SingleIdAsync(Organizacao.Id);
=======
            var _unidade = _mapper.Map<Sim.Domain.Entity.Secretaria>(Input);
            _unidade.Owner = await _appServicePrefeitura.SingleIdAsync(Organizacao.Id);
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
            _unidade.Ativo = true;
            if (ModelState.IsValid)
                await _appSecretaria.AddAsync(_unidade);  

<<<<<<< HEAD
            await OnLoad(_unidade.Id);
=======
            await OnLoad(_unidade.Owner.Id);
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
        }
        catch (Exception ex){
            StatusMessage = "Erro: ao tentar incluir Unidade!" + "\n" + ex.Message;
        }      
    }
}

