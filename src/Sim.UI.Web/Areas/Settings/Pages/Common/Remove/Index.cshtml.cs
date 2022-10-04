using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Remove;

public class IndexModel : PageModel
{
<<<<<<< HEAD
    private readonly IAppServiceSecretaria _appservice;
    public IndexModel(IAppServiceSecretaria appServicPref)
    {
        _appservice = appServicPref;
=======
    private readonly IAppServicePrefeitura _appservicePrefeitura;
    public IndexModel(IAppServicePrefeitura appServicPref)
    {
        _appservicePrefeitura = appServicPref;
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
    }

    [TempData]
    public string StatusMessage { get; set; }
    public async Task<IActionResult> OnGetAsync(string id) {
        try
        {
<<<<<<< HEAD
            var canal = await _appservice.SingleIdAsync(new Guid(id));
            await _appservice.RemoveAsync(canal);
=======
            var canal = await _appservicePrefeitura.SingleIdAsync(new Guid(id));
            await _appservicePrefeitura.RemoveAsync(canal);
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
        }
        catch (Exception ex)
        {
            StatusMessage = "Erro ao tentar remover a Organização!" + "\n" + ex.Message;
        }
        return RedirectToPage("/Common/Index");
    }
}