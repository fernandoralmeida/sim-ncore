using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.VM;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Setor;

public class IndexModel : PageModel
{
    private readonly IAppServicePrefeitura _appPrefeitura;
    private readonly IAppServiceSecretaria _appSecretaria;
    private readonly IAppServiceSetor _appSetor;
    private readonly IMapper _mapper;
    public IndexModel(IAppServiceSetor appSetor,
        IAppServiceSecretaria appSecretaria,
        IAppServicePrefeitura appPrefeitura,
        IMapper mapper)
    {
        _appSetor = appSetor;
        _appSecretaria = appSecretaria;
        _appPrefeitura = appPrefeitura;
        _mapper = mapper;
    }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public VMSetor Input { get; set; }

    [BindProperty]
    public VMSecretaria Unidade { get; set; }

    [BindProperty]
    public VMPrefeitura Organizacao { get; set; }

    public IEnumerable<Sim.Domain.Entity.Setor> Setores { get; set; }

    private async Task OnLoad(Guid id)
    {
        Setores = await _appSetor.ListAllAsync();
        Unidade = _mapper.Map<VMSecretaria>(await _appSecretaria.GetIdAsync(id));          
    }

    public async Task OnGetAsync(string id, string og)
    {
        if (!string.IsNullOrEmpty(id)) 
            await OnLoad(new Guid(id));      

        if (!string.IsNullOrEmpty(og)) 
            Organizacao = _mapper.Map<VMPrefeitura>(await _appPrefeitura.GetByIdAsync(new Guid(og)));  
    }

    public async Task OnPostAsync()
    {
        try{
            var _setor = _mapper.Map<Sim.Domain.Entity.Setor>(Input);
            _setor.Secretaria = await _appSecretaria.SingleIdAsync(Unidade.Id);
            _setor.Ativo = true;
            if (ModelState.IsValid)
                await _appSetor.AddAsync(_setor);  

            await OnLoad(_setor.Secretaria.Id);
        }
        catch (Exception ex){
            StatusMessage = "Erro: ao tentar incluir Unidade!" + "\n" + ex.Message;
        }      
    }
}


