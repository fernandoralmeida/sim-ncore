using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Application.Interfaces;
using Sim.Domain.Evento.Model;

namespace Sim.UI.Web.Areas.SimBI.Pages.Clientes
{ 
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEvento _appevento;

        [BindProperty(SupportsGet = true)]
        public EBIEventos LEventos { get; set; }

        [BindProperty]
        public int InputAno { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public IndexModel(IAppServiceEvento appevento)  {
            _appevento = appevento;    
        }
        public async Task OnGetAsync()
        {
            StatusMessage = "";
            InputAno = DateTime.Today.Year;
            var _list = await _appevento.DoListAsync(s => s.Data.Value.Year == InputAno);
            LEventos = await _appevento.DoBIEventosAsync(_list);
        }

        public async Task OnPostAsync()
        {
            var _list = await _appevento.DoListAsync(s => s.Data.Value.Year == InputAno);
            LEventos = await _appevento.DoBIEventosAsync(_list);
        }
    }
}
