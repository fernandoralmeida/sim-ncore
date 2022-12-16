using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sim.UI.Web.Pages.BancoPovo.Consulta;

[Authorize(Roles = "Admin_Global,M_BancoPovo,M_BancoPovo_Admin")]
public class IndexModel : PageModel
{
    [BindProperty]
    public string Search { get; set;}
    public void OnGet()
    {
    }
}

