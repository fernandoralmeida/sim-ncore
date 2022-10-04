using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Servicos;

public class IndexModel : PageModel
{
    [TempData]
    public string StatusMessage { get; set; }
    public void OnGet()
    {
    }
}

