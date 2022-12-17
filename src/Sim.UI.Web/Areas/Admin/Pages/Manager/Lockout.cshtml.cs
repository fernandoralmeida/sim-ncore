using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.UI.Web.Areas.Admin.ViewModel;
using Sim.Identity.Interfaces;

namespace Sim.UI.Web.Areas.Admin.Pages.Manager
{

    [Authorize(Roles = "Administrador,Admin_Global,Admin_Account")]
    public class LockoutModel : PageModel
    {
        private readonly IServiceUser _appIdentity;
        public LockoutModel(IServiceUser appServiceUser)
        {
            _appIdentity = appServiceUser;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public VMListUsers Input { get; set; }

        private async Task LoadAsync()
        {   
            var _lockout = await _appIdentity.ListAllAsync();
            Input = new() {
                Users = _lockout.Where(s => s.LockoutEnabled == true).OrderBy(o => o.UserName)
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
