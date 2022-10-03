using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Remove;

public class IndexModel : PageModel
{
    private readonly IAppServicePrefeitura _appservicePrefeitura;
    public IndexModel(IAppServicePrefeitura appServicPref)
    {
        _appservicePrefeitura = appServicPref;
    }

    [TempData]
    public string StatusMessage { get; set; }
    public async Task<IActionResult> OnGetAsync(string id) {
        try
        {
            var canal = await _appservicePrefeitura.SingleIdAsync(new Guid(id));
            await _appservicePrefeitura.RemoveAsync(canal);
        }
        catch (Exception ex)
        {
            StatusMessage = "Erro ao tentar remover a Organização!" + "\n" + ex.Message;
        }
        return RedirectToPage("/Common/Index");
    }
}