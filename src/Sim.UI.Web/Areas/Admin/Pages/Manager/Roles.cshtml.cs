using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Sim.UI.Web.Areas.Admin.ViewModel;

namespace Sim.UI.Web.Areas.Admin.Pages.Manager
{

    [Authorize(Roles = "Administrador,Admin_Global")]
    public class RolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public VMRoles Input { get; set; }

        private async Task LoadAsync()
        {
            var t = Task.Run(() => _roleManager.Roles.OrderBy(o => o.Name));
            await t;
            Input = new() {
                Roles = t.Result
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = new IdentityRole(Input.Name);
                    var roleresult = await _roleManager.CreateAsync(role);

                    if (roleresult.Succeeded)
                    {
                        Input.Roles = _roleManager.Roles;
                        Input.Name = string.Empty;
                        return Page();
                    }
                    return Page();
                }
                return Page();
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                return Page();
            }
        }

        public async Task<IActionResult> OnPostRemoveAsync(string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = await  _roleManager.FindByIdAsync(id);
                    
                    if (role == null)
                        return Page();

                    var delete = await _roleManager.DeleteAsync(role);


                    if (!delete.Succeeded)
                    {
                        StatusMessage = delete.Errors.First().ToString();
                        return Page();
                    }
                    return RedirectToPage();

                }

                return Page();
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                return Page();
            }
        }
    }
}
