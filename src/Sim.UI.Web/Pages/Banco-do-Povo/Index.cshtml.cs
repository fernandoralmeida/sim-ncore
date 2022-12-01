using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.BancoPovo.Interfaces;
using Sim.Domain.BancoPovo.Models;

namespace Sim.UI.Web.Pages.BancoPovo;

[Authorize(Roles = "Administrador,M_BancoPovo")]
public class IndexModel : PageModel {

    private readonly IMapper _mapper;
    private readonly IAppServiceContratos _appcontratos;

    [BindProperty]
    public IEnumerable<EContrato> MeusContratos { get; set; }

    public IndexModel (IMapper mapper,
        IAppServiceContratos appServiceContratos) {
            _mapper = mapper;
            _appcontratos = appServiceContratos;        
    }
    public async Task OnGetAsync(string p, string src) {        
        MeusContratos = await _appcontratos.DoListAsync(s => s.AppUser == User.Identity.Name);
        MeusContratos.OrderByDescending(o => o.Data);
    }
}

