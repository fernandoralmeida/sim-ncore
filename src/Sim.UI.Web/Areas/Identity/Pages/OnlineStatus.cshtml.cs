using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sim.UI.Web.Areas.Identity.Pages
{
    [Authorize]
    public class OnlineStatusModel : PageModel
    {
        private readonly IAppServiceStatusAtendimento _statusAtendimento;

        public OnlineStatusModel(IAppServiceStatusAtendimento statusAtendimento)
        {
            _statusAtendimento = statusAtendimento;
        }

        public IActionResult OnGet(string id, bool val)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var gid = new Guid(id);

                var meustatus = _statusAtendimento.GetIdAsync(gid).Result;
                meustatus.Online = val;
                 _statusAtendimento.UpdateAsync(meustatus).Wait();
            }

            return RedirectToPage();
        }

    }
}
