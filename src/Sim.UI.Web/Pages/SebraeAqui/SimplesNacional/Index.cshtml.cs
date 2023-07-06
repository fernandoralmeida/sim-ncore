using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Domain.Customer.Models;
using Sim.Application.Customer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Sim.Application.Sebrae.Interfaces;
using Sim.Domain.Sebrae.Model;

namespace Sim.UI.Web.Pages.SebraeAqui.SimplesNacional;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IAppServiceSimples _simples;

    public bool _result = false;

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Search { get; set; }

    public IEnumerable<ESimples> Listar { get; set; }

    public IndexModel(IAppServiceSimples repository)
    {
        _simples = repository;
    }

    public async Task OnGetAsync()
        => await Task.Run(async () =>
        {
            Listar = await _simples.DoListAsync(s => s.Id == new Guid());
        });

    public async Task OnPostAsync()
        => Listar = await _simples.DoListAsync(s => s.Empresa.CNPJ == Search);

    public async Task<JsonResult> OnGetDelete(Guid id)
    {
        var _result = await _simples.DoListAsync(s => s.Id == id);
        await _simples.RemoveAsync(await _simples.SingleIdAsync(id));
        return new JsonResult(_result);
    }


}