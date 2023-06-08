using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Domain.Customer.Models;
using Sim.Application.Customer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.UI.Web.Functions;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Pages.Bind;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IAppServiceBindings _bindings;

    public bool _result = false;

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Search { get; set; }

    public IEnumerable<EBindings> Listar { get; set; }

    public IndexModel(IAppServiceBindings repository)
    {
        _bindings = repository;
    }

    public async Task OnGetAsync()
        => await Task.Run(async () =>
        {
            Listar = await _bindings.DoListAsync();
        });

    public async Task OnPostAsync()
        => Listar = await _bindings.DoListAsync(s => s.Pessoa.CPF == Search || s.Empresa.CNPJ == Search);


}