using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Cnpj.Views;
using Sim.Application.Cnpj.Interfaces;
using AutoMapper;

namespace Sim.UI.Web.Pages.Empresa.Preview;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IAppServiceCnpj _empresaApp;
    private readonly IMapper _mapper;

    public IndexModel(IAppServiceCnpj appServiceEmpresa,
        IMapper mapper)
    {
        _empresaApp = appServiceEmpresa;
        _mapper = mapper;
    }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public VMBaseReceitaFederal Input { get; set; }

    public async Task OnGetAsync(string id) =>
        Input = _mapper.Map<VMBaseReceitaFederal>(await _empresaApp.GetCNPJAsync(id)); 
}

