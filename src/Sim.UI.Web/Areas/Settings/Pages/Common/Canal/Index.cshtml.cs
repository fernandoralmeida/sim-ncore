using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Interfaces;
using Sim.Application.VM;
using Sim.Domain.Organizacao.Model;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Canal;

public class IndexModel : PageModel
{
    //private readonly IAppServiceSetor _appServiceSetor;
    private readonly IAppServiceSecretaria _appServiceSecretaria;
    private readonly IAppServiceCanal _appServiceCanal;
    public IndexModel(IAppServiceSecretaria appServiceSecretaria,
        IAppServiceCanal appServiceCanal)
    {
        //_appServiceSetor = appServiceSetor;
        _appServiceSecretaria = appServiceSecretaria;
        _appServiceCanal = appServiceCanal;
    }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public VMCanal Input { get; set; }
    public VMSecretaria Organizacao { get; set; }

    public IEnumerable<ECanal> Canais { get; set; }
    public SelectList Setores { get; set; }
    public SelectList Unidades { get; set; }

    private async Task OnLoad()
    {
        Unidades = new SelectList(
            await _appServiceSecretaria.ListAllAsync(),
            nameof(EOrganizacao.Id),
            nameof(EOrganizacao.Nome),
            null);

        Setores = new SelectList(
            await _appServiceSecretaria.ListAllAsync(),
            nameof(EOrganizacao.Id),
            nameof(EOrganizacao.Nome),
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
                var _sec = await _appServiceSecretaria.SingleIdAsync(Input.Id);
                //var set = await _appServiceSetor.SingleIdAsync(Input.Setor.Id);

                await _appServiceCanal.AddAsync(
                    new ECanal(){
                        Nome = Input.Nome,
                        Dominio = _sec,
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
            var canal = await _appServiceCanal.GetIdAsync(id);
            await _appServiceCanal.RemoveAsync(canal);
            await OnLoad();
        }
        catch (Exception ex)
        {
            StatusMessage = "Erro ao tentar remover canal!" + "\n" + ex.Message;
        }
    }
}

