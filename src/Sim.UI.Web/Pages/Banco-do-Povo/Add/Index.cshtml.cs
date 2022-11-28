using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.VM;

namespace Sim.UI.Web.Pages.BancoPovo.Add;

[Authorize(Roles = "Administrador,M_BancoPovo")]
public class IndexModel : PageModel
{   
    private readonly IMapper _mapper;
    private readonly IAppServiceEmpresa _appServiceEmpresa;
    private readonly IAppServicePessoa _appServicePessoa;

    [TempData]
    public string StatusMessage { get; set; }
    
    [BindProperty]
    public IEnumerable<VMPessoa> ListaPessoas { get; set;}
    
    [BindProperty]
    public VMPessoa InputPessoa { get; set;}

    [BindProperty]
    public string Search {get; set;}
    public IndexModel(IMapper mapper,
        IAppServiceEmpresa appempresa,
        IAppServicePessoa apppessoa)
    {
        _mapper = mapper;
        _appServiceEmpresa = appempresa;
        _appServicePessoa = apppessoa;
    }

    public void OnGet()
    {
    }

    public async Task OnPostAsync() {
        if(Search != null) {
            var p = await _appServicePessoa.ConsultaCPFAsync(Search);
            ListaPessoas = _mapper.Map<IEnumerable<VMPessoa>>(p);            
        }        
    }
}

