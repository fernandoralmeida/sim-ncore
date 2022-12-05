using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sim.UI.Web.Pages.BancoPovo.Reports;

[Authorize(Roles = "Administrador,M_BancoPovo")]
public class IndexModel : PageModel
{
    [BindProperty]
    public string Search { get; set;}
    public void OnGet()
    {
    }
}

