using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.BancoPovo.Interfaces;
using Sim.Domain.BancoPovo.Models;

namespace Sim.UI.Web.Pages.BancoPovo.Renegociados;

[Authorize(Roles = "Administrador,M_BancoPovo")]
public class IndexModel : PageModel {

    private readonly IMapper _mapper;
    private readonly IAppServiceContratos _appcontratos;

    [BindProperty]
    public IEnumerable<EContrato> MeusContratos { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    public IndexModel (IMapper mapper,
        IAppServiceContratos appServiceContratos) {
            _mapper = mapper;
            _appcontratos = appServiceContratos;        
    }
    public async Task OnGetAsync() {        
        MeusContratos = await _appcontratos.DoListAsync(s => s.AppUser == User.Identity.Name && s.Situacao == EContrato.EnSituacao.Aprovado && s.Renegociacaoes.Count() > 0);     
        MeusContratos.OrderByDescending(o => o.Data);
    }

    public async Task OnPostAsync(string src) {
        var _list = await _appcontratos.DoListAsync(s => s.AppUser == User.Identity.Name && s.Situacao == EContrato.EnSituacao.Documentacao);
        MeusContratos = _list.Where(s => s.Empresa.Nome_Empresarial.Contains(src) || s.Cliente.Nome.Contains(src));
        MeusContratos.OrderByDescending(o => o.Data);
    }
}

