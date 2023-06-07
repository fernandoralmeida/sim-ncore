using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sim.UI.Web.Pages.BancoPovo.Consulta;

[Authorize(Roles = $"{Web.Areas.Admin.Pages.Admin.Global},SEDEMPI Banco do Povo")]
public class IndexModel : PageModel
{
    [BindProperty]
    public string Search { get; set;}
    public void OnGet()
    {
    }
}

