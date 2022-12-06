using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sim.UI.Web.Pages.Pat.Reports;

[Authorize(Roles = "Administrador")]
public class IndexModel : PageModel {
    
    public IndexModel(){

    }

    public void OnGet(){

    }
}
