using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.UI.Web.Areas.Admin.ViewModel;
using Sim.Application.Interfaces;
using Sim.Domain.Organizacao.Model;

namespace Sim.UI.Web.Areas.Admin.Pages.Manager
{

    [Authorize(Roles = $"{Admin.Global}")]
    public class RolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAppServiceSecretaria _appOrg;

        public RolesModel(RoleManager<IdentityRole> roleManager,
            IAppServiceSecretaria appOrg)
        {
            _roleManager = roleManager;
            _appOrg = appOrg;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public VMRoles Input { get; set; }

        [BindProperty]
        public string Funcao { get; set; }

        [BindProperty]
        public string Org { get; set; }

        [BindProperty]
        public string Division { get; set; }

        [BindProperty]
        public string Modulo { get; set; }

        public SelectList Funcoes { get; set; }
        public SelectList Orgs { get; set; }
        public SelectList Divisions { get; set; }
        public SelectList Modulos { get; set; }

        private async Task LoadAsync()
        {
            var t = Task.Run(() => _roleManager.Roles.OrderBy(o => o.Name));
            await t;
            Input = new()
            {
                Roles = t.Result
            };

            var _orglist = new List<string>();
            var _divlist = new List<string>();
            var _modlist = new List<string>();
            var _modlist2 = new List<string>();

            var _list = await _appOrg.DoList();

            foreach (var item0 in await _appOrg.DoListHierarquia0Async(_list))
            {
                _orglist.Add(item0.Acronimo);
                foreach (var item1 in await _appOrg.DoListHierarquia1from0Async(_list, item0.Id))
                {
                    _divlist.Add(item1.Acronimo);
                    foreach (var item2 in await _appOrg.DoListHierarquia2from1Async(_list.Where(s => s.Acronimo != item1.Acronimo), item1.Id))
                    {
                        _modlist.Add(item2.Acronimo);
                        _modlist2.Add(item2.Acronimo);
                    }
                }
            }

            var _funcs = new List<string>();
            _funcs = Admin.ToList().ToList();
            foreach (var l in Admin.ToList())
                foreach (var sl in Input.Roles.Where(s => s.Name == l))
                    _funcs.Remove(sl.Name);

            foreach (var d in _divlist)            
                foreach (var l in _modlist2)
                    foreach (var sl in Input.Roles.Where(s => s.Name.Contains(l)))
                        _modlist.Remove(sl.Name.Remove(0, d.Length).Trim());
            

            Funcoes = new SelectList(_funcs);
            Orgs = new SelectList(_orglist, nameof(EOrganizacao.Acronimo));
            Divisions = new SelectList(_divlist, nameof(EOrganizacao.Acronimo));
            Modulos = new SelectList(_modlist);
            Input.Roles.OrderBy(o => o.Name);
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
                await LoadAsync();
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

        public async Task<IActionResult> OnPostAddFuncAsync()
        {
            try
            {
                await LoadAsync();
                if (ModelState.IsValid)
                {
                    var role = new IdentityRole(Funcao);
                    var roleresult = await _roleManager.CreateAsync(role);

                    if (roleresult.Succeeded)
                    {
                        Input.Roles = _roleManager.Roles;
                        Funcao = string.Empty;

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

        public async Task<IActionResult> OnPostAddModAsync()
        {
            try
            {
                await LoadAsync();
                if (ModelState.IsValid)
                {
                    var role = new IdentityRole($"{Division} {Modulo}");
                    var roleresult = await _roleManager.CreateAsync(role);

                    if (roleresult.Succeeded)
                    {
                        Input.Roles = _roleManager.Roles;
                        Modulo = string.Empty;
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
                await LoadAsync();
                if (ModelState.IsValid)
                {
                    var role = await _roleManager.FindByIdAsync(id);

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
