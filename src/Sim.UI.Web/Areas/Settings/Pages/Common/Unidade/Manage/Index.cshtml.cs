using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.VM;
using Sim.Domain.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Unidade.Manage;
public class IndexModel : PageModel
{
    //private readonly IAppServicePrefeitura _appServicePrefeitura;
    private readonly IAppServiceSecretaria _appSecretaria;
    private readonly IMapper _mapper;
    public IndexModel(IAppServiceSecretaria appSecretaria,
        IMapper mapper)
    {
        //_appServicePrefeitura = appService;
        _appSecretaria = appSecretaria;
        _mapper = mapper;
    }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    public VMSecretaria Input { get; set; }

    [BindProperty(SupportsGet = true)]
    public VMSecretaria Organizacao { get; set; }

    private async Task OnLoad(string param) =>    
        Input = _mapper.Map<VMSecretaria>(await _appSecretaria.SingleIdAsync(new Guid(param)));  

    public async Task OnGetAsync(string id, string og)
    {
       await OnLoad(id);  
       if (!string.IsNullOrEmpty(og))
            Organizacao = _mapper.Map<VMSecretaria>(await _appSecretaria.SingleIdAsync(new Guid(og))); 
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {   
            var _und = await _appSecretaria.GetIdAsync(Input.Id);
            //var _org = await _appServicePrefeitura.SingleIdAsync(Organizacao.Id);
            _und.Nome = Input.Nome;
            _und.Acronimo = Input.Acronimo;
            _und.Dominio = Input.Dominio;
            await _appSecretaria.UpdateAsync(_und);

            return RedirectToPage("/Common/Unidade/Index", new { id = _und.Id } );
        }

        return Page();
    }
}