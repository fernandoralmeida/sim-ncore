using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.UI.Web.Areas.Admin.ViewModel;
using Sim.Identity.Interfaces;

namespace Sim.UI.Web.Areas.Admin.Pages.Manager
{

    [Authorize(Roles = "Administrador,Admin_Global,Admin_Account")]
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
            Input = new()
            {
                Users = await _appIdentity.ListAllAsync()
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnGetLockUnlock(string id, bool blk) {            
            var _status = await _appIdentity.lockUnlockAsync(id, blk);
            return Page();                       
        }
    }
}
