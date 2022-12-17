using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.UI.Web.Areas.Admin.ViewModel;
using Sim.Identity.Interfaces;
using Sim.Identity.Entity;

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

        public IEnumerable<ApplicationUser> Users_Admin_Global { get; set; }
        public IEnumerable<ApplicationUser> Users_Admin_Account { get; set; }
        public IEnumerable<ApplicationUser> Users_Admin_Config { get; set; }

        private async Task LoadAsync()
        { 
            Users_Admin_Global = new List<ApplicationUser>();
            Users_Admin_Account = new List<ApplicationUser>();
            Users_Admin_Config = new List<ApplicationUser>();
            
            var _lockout_off = await _appIdentity.ListAllAsync();
            Input = new() {
                Users = _lockout_off.Where(s => s.LockoutEnabled == false).OrderBy(o => o.UserName)
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
