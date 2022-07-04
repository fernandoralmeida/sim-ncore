using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

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

        public void OnGet(string id, bool val)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var gid = new Guid(id);

                var meustatus = _statusAtendimento.GetIdAsync(gid).Result;
                meustatus.Online = val;
                 _statusAtendimento.UpdateAsync(meustatus).Wait();
            }
        }

    }
}
