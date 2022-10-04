using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Interfaces;
using Sim.Application.VM;
using Sim.Domain.Organizacao.Model;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Parceiros;

public class IndexModel : PageModel
{

    private readonly IAppServiceSecretaria _appServiceSecretaria;
    private readonly IAppServiceParceiro _appServiceParceiro;
    public IndexModel(IAppServiceSecretaria appServiceSecretaria,
        IAppServiceParceiro appServiceParceiro)
    {
        _appServiceSecretaria = appServiceSecretaria;
        _appServiceParceiro = appServiceParceiro;
    }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public VMParceiros Input { get; set; }
    public IEnumerable<EParceiro> Parceiros { get; set; }
    public SelectList Secretarias { get; set; }

    private async Task OnLoad()
    {
        Secretarias = new SelectList(
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
                var sec = await _appServiceSecretaria.GetIdAsync(Input.Id);

                await _appServiceParceiro.AddAsync(
                    new EParceiro(){
                        Nome = Input.Nome,
                        Dominio = sec,
                        Ativo = true
                    }
                );

                Input.Nome = string.Empty;
            }
            await OnLoad();
        }
        catch (Exception ex)
        {
            StatusMessage = "Erro ao tentar incluir novo parceiro!" + "\n" + ex.Message;
        }

    }

    public async Task OnPostRemoveAsync(Guid id)
    {
        try
        {
            var canal = await _appServiceParceiro.GetIdAsync(id);

            await _appServiceParceiro.RemoveAsync(canal);

            await OnLoad();

        }
        catch (Exception ex)
        {
            StatusMessage = "Erro ao tentar remover parceiro!" + "\n" + ex.Message;
        }
    }
}

