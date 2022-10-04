using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
<<<<<<< HEAD
using Sim.Domain.Organizacao.Model;
=======
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
using Sim.Application.Interfaces;
using Sim.Application.VM;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Setor;

public class IndexModel : PageModel
{
<<<<<<< HEAD
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
=======
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
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
        _mapper = mapper;
    }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
<<<<<<< HEAD
    public VMSecretaria Input { get; set; }
=======
    public VMSetor Input { get; set; }
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd

    [BindProperty]
    public VMSecretaria Unidade { get; set; }

    [BindProperty]
<<<<<<< HEAD
    public VMSecretaria Organizacao { get; set; }

    public IEnumerable<EOrganizacao> Setores { get; set; }

    private async Task OnLoad(Guid id)
    {
        //Setores = await _appSetor.ListAllAsync();
=======
    public VMPrefeitura Organizacao { get; set; }

    public IEnumerable<Sim.Domain.Entity.Setor> Setores { get; set; }

    private async Task OnLoad(Guid id)
    {
        Setores = await _appSetor.ListAllAsync();
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
        Unidade = _mapper.Map<VMSecretaria>(await _appSecretaria.GetIdAsync(id));          
    }

    public async Task OnGetAsync(string id, string og)
    {
        if (!string.IsNullOrEmpty(id)) 
            await OnLoad(new Guid(id));      

        if (!string.IsNullOrEmpty(og)) 
<<<<<<< HEAD
            Organizacao = _mapper.Map<VMSecretaria>(await _appSecretaria.GetIdAsync(new Guid(og)));  
=======
            Organizacao = _mapper.Map<VMPrefeitura>(await _appPrefeitura.GetByIdAsync(new Guid(og)));  
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
    }

    public async Task OnPostAsync()
    {
        try{
<<<<<<< HEAD
            var _setor = _mapper.Map<EOrganizacao>(Input);
            _setor = await _appSecretaria.SingleIdAsync(Unidade.Id);
            _setor.Ativo = true;
            if (ModelState.IsValid)
                await _appSecretaria.AddAsync(_setor);  

            await OnLoad(_setor.Id);
=======
            var _setor = _mapper.Map<Sim.Domain.Entity.Setor>(Input);
            _setor.Secretaria = await _appSecretaria.SingleIdAsync(Unidade.Id);
            _setor.Ativo = true;
            if (ModelState.IsValid)
                await _appSetor.AddAsync(_setor);  

            await OnLoad(_setor.Secretaria.Id);
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
        }
        catch (Exception ex){
            StatusMessage = "Erro: ao tentar incluir Unidade!" + "\n" + ex.Message;
        }      
    }
}


