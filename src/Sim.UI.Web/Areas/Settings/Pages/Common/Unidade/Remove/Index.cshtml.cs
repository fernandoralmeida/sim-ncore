using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Unidade.Remove;

public class IndexModel : PageModel
{
<<<<<<< HEAD
    //private readonly IAppServicePrefeitura _appservicePrefeitura;
    private readonly IAppServiceSecretaria _appSecretaria;
    public IndexModel(IAppServiceSecretaria appSecretaria)
    {
        //_appservicePrefeitura = appServicPref;
=======
    private readonly IAppServicePrefeitura _appservicePrefeitura;
    private readonly IAppServiceSecretaria _appSecretaria;
    public IndexModel(IAppServicePrefeitura appServicPref,
        IAppServiceSecretaria appSecretaria)
    {
        _appservicePrefeitura = appServicPref;
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
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