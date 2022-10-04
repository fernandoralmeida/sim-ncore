using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Domain.Organizacao.Model;
using Sim.Application.Interfaces;
using Sim.Application.VM;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Setor;

public class IndexModel : PageModel
{
    //private readonly IAppServicePrefeitura _appPrefeitura;
    private readonly IAppServiceSecretaria _appSecretaria;
    //private readonly IAppServiceSetor _appSetor;
    private readonly IMapper _mapper;
    public IndexModel(IAppServiceSecretaria appSecretaria,
        IMapper mapper)
    {
        //_appSetor = appSetor;
        _appSecretaria = appSecretaria;
        //_appPrefeitura = appPrefeitura;
        _mapper = mapper;
    }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public VMSecretaria Input { get; set; }

    [BindProperty]
    public VMSecretaria Unidade { get; set; }

    [BindProperty]
    public VMSecretaria Organizacao { get; set; }

    public IEnumerable<EOrganizacao> Setores { get; set; }

    private async Task OnLoad(Guid id)
    {
        //Setores = await _appSetor.ListAllAsync();
        Unidade = _mapper.Map<VMSecretaria>(await _appSecretaria.GetIdAsync(id));          
    }

    public async Task OnGetAsync(string id, string og)
    {
        if (!string.IsNullOrEmpty(id)) 
            await OnLoad(new Guid(id));      

        if (!string.IsNullOrEmpty(og)) 
            Organizacao = _mapper.Map<VMSecretaria>(await _appSecretaria.GetIdAsync(new Guid(og)));  
    }

    public async Task OnPostAsync()
    {
        try{
            var _setor = _mapper.Map<EOrganizacao>(Input);
            _setor = await _appSecretaria.SingleIdAsync(Unidade.Id);
            _setor.Ativo = true;
            if (ModelState.IsValid)
                await _appSecretaria.AddAsync(_setor);  

            await OnLoad(_setor.Id);
        }
        catch (Exception ex){
            StatusMessage = "Erro: ao tentar incluir Unidade!" + "\n" + ex.Message;
        }      
    }
}


