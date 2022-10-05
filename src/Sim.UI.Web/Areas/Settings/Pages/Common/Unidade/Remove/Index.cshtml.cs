using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Unidade.Remove;

public class IndexModel : PageModel
{
    //private readonly IAppServicePrefeitura _appservicePrefeitura;
    private readonly IAppServiceSecretaria _appSecretaria;
    public IndexModel(IAppServiceSecretaria appSecretaria)
    {
        //_appservicePrefeitura = appServicPref;
        _appSecretaria = appSecretaria;
    }

    [TempData]
    public string StatusMessage { get; set; }
    public async Task<IActionResult> OnGetAsync(string id, string og) {
        try
        {   
            var _unidade = await _appSecretaria.SingleIdAsync(new Guid(id));         
            await _appSecretaria.RemoveAsync(_unidade);            
        }
        catch (Exception ex)
        {
            StatusMessage = "Erro ao tentar remover a Unidade!" + "\n" + ex.Message;
        }
        return RedirectToPage("/Common/Unidade/Index", new { id = og } );
    }
}