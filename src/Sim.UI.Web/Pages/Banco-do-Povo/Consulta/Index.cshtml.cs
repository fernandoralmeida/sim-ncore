using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sim.UI.Web.Pages.BancoPovo.Consulta;
public class IndexModel : PageModel
{
    [BindProperty]
    public string Search { get; set;}
    public void OnGet()
    {
    }
}

