using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Identity.Entity;
using Sim.Identity.Interfaces;

namespace Sim.UI.Web.Areas.Identity.Pages.Account.Manage
{

    [Authorize]
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IServiceUser _user;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IServiceUser user)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _user = user;
        }

        public string UserName { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Nome")]
            public string Name { get; set; }

            [Display(Name = "Sobrenome")]
            public string LastName { get; set; }

            [Display(Name = "Gênero")]
            public string Genero { get; set; }

            [Phone]
            [Display(Name = "Telefone")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var appuser = await _userManager.FindByNameAsync(userName);

            UserName = userName;

            Input = new InputModel
            {
                Name = appuser.Name,
                LastName = appuser.LastName,
                Genero = appuser.Gender,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetSetTheme(string id, string theme)
        {
            if (!string.IsNullOrEmpty(id))
            {
                await _user.SetThemeAsync(id, theme);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível carregar o usuário com ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível carregar o usuário com ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Erro inesperado ao tentar definir o número de telefone.";
                    return RedirectToPage();
                }
            }

            var name_lastname = await _userManager.GetUserAsync(User);
            if(Input.Name != name_lastname.Name || Input.LastName != name_lastname.LastName || Input.Genero != name_lastname.Gender)
            {
                name_lastname.Name = Input.Name;
                name_lastname.LastName = Input.LastName;
                name_lastname.Gender = Input.Genero;
                var update_user = await _userManager.UpdateAsync(name_lastname);
                if(!update_user.Succeeded)
                {
                    StatusMessage = "Erro inesperado, tente novamente.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Seu perfil foi atualizado";
            return RedirectToPage();
        }
    }
}
