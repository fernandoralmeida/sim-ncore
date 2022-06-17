using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.UI.Web.Areas.Admin.ViewModel;
using Sim.Identity.Interfaces;

namespace Sim.UI.Web.Areas.Admin.Pages.Manager
{

    [Authorize(Roles = "Administrador")]
    public class IndexModel : PageModel
    {
        private readonly IServiceUser _appIdentity;
        public IndexModel(IServiceUser appServiceUser)
        {
            _appIdentity = appServiceUser;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public VMListUsers Input { get; set; }

        private async Task LoadAsync()
        {
            Input.Users = await _appIdentity.ListAllAsync();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }
    }
}
