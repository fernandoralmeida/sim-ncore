using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using Sim.UI.Web.Areas.Admin.ViewModel;
using Sim.Identity.Entity;

namespace Sim.UI.Web.Areas.Admin.Pages.Manager
{

    [Authorize(Roles = "Admin_Global,Admin_Account")]
    public class UserRolesModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public UserRolesModel(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public VMUserRoles Input { get; set; }

        [BindProperty]
        public string ResetCode { get; set; }

        [Required]
        [BindProperty]   
        public string Selecionado { get; set; }

        public SelectList RoleList { get; set; }

        private async Task LoadAsync(string id)
        {
            var roles = _roleManager.Roles.ToList();
            if (User.IsInRole("Admin_Global"))
                RoleList = new SelectList(roles.OrderBy(o => o.Name), nameof(IdentityRole.Name));
            else
                RoleList = new SelectList(roles.Where(s => s.Name != "Admin_Global" && s.Name != "Admin_Account" && s.Name != "Administrador").OrderBy(o => o.Name),nameof(IdentityRole.Name));                

            var u = await _userManager.FindByIdAsync(id);

            var r = await _userManager.GetRolesAsync(u);
            Input = new()
            {
                Id = u.Id,
                UserName = u.UserName,
                Name = u.Name,
                LastName = u.LastName,
                Gender = u.Gender,
                Email = u.Email,
                EmailConfirmed = u.EmailConfirmed,
                ListRoles = r
            };
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await LoadAsync(id);
            var user = await _userManager.FindByEmailAsync(Input.Email);
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            ResetCode = code;
            return Page();
        }

        public async Task<IActionResult> OnPostConfirmEmailAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _userManager.ConfirmEmailAsync(user, code);
                
                return RedirectToPage("./UserRoles", new { id });
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                await LoadAsync(id);
                return Page();
            }

        }

        public async Task<IActionResult> OnPostAddRoleAsync(string id)
        {
            try {
                var user = await _userManager.FindByIdAsync(id);

                await _userManager.AddToRoleAsync(user, Selecionado);

                return RedirectToPage("./UserRoles", new { id });
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                await LoadAsync(id);
                return Page();
            }

        }

        public async Task<IActionResult> OnPostRemoveRoleAsync(string id, string role)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                await _userManager.RemoveFromRoleAsync(user, role);

                return RedirectToPage("./UserRoles", new { id = user.Id });
            }
            catch
            {
                return Page();
            }
        }
    }
}
