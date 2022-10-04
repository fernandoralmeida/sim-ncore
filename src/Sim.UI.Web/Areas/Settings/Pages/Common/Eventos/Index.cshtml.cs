using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Interfaces;
using Sim.Application.VM;
<<<<<<< HEAD
using Sim.Domain.Evento.Model;
using Sim.Domain.Organizacao.Model;
=======
using Sim.Domain.Entity;
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Eventos;

public class IndexModel : PageModel
{
<<<<<<< HEAD
    //private readonly IAppServiceSetor _appServiceSetor;
    private readonly IAppServiceSecretaria _appServiceSecretaria;
    private readonly IAppServiceTipo _appServiceTipo;
    public IndexModel(IAppServiceSecretaria appServiceSecretaria,
        IAppServiceTipo appServiceTipo)
    {
        //_appServiceSetor = appServiceSetor;
=======
    private readonly IAppServiceSetor _appServiceSetor;
    private readonly IAppServiceSecretaria _appServiceSecretaria;
    private readonly IAppServiceTipo _appServiceTipo;
    public IndexModel(IAppServiceSetor appServiceSetor,
        IAppServiceSecretaria appServiceSecretaria,
        IAppServiceTipo appServiceTipo)
    {
        _appServiceSetor = appServiceSetor;
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
        _appServiceSecretaria = appServiceSecretaria;
        _appServiceTipo = appServiceTipo;
    }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public VMTipo Input { get; set; }

    public IEnumerable<ETipo> Tipos { get; set; }
    public SelectList Unidades { get; set; }

    private async Task OnLoad()
    {
        Unidades = new SelectList(
            await _appServiceSecretaria.ListAllAsync(),
<<<<<<< HEAD
            nameof(EOrganizacao.Id),
            nameof(EOrganizacao.Nome),
=======
            nameof(Secretaria.Id),
            nameof(Secretaria.Nome),
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
            null);
    }

    public async Task<IActionResult> OnGetAsync()
    {
        await OnLoad();
        return Page();
    }

    public async Task OnPostAddAsync()
    {
        try
        {
            if (ModelState.IsValid)
            {  
                await _appServiceTipo.AddAsync(
                    new ETipo(){
                        Nome = Input.Nome,
                        Tipo = "Evento",
                        Ativo = true
                    });                    
            }
            await OnLoad();
            Input.Nome = string.Empty;
        }
        catch (Exception ex)
        {
            StatusMessage = "Erro ao tentar incluír novo canal!" + "\n" + ex.Message;
        }
    }

    public async Task OnPostRemoveAsync(Guid id)
    {
        try
        {
            var canal = await _appServiceTipo.GetIdAsync(id);
            await _appServiceTipo.RemoveAsync(canal);
            await OnLoad();
        }
        catch (Exception ex)
        {
            StatusMessage = "Erro ao tentar remover canal!" + "\n" + ex.Message;
        }
    }
}

