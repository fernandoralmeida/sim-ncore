using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Interfaces;
using Sim.Application.VM;
<<<<<<< HEAD
using Sim.Domain.Organizacao.Model;
=======
using Sim.Domain.Entity;
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd

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
<<<<<<< HEAD
    public IEnumerable<EParceiro> Parceiros { get; set; }
=======
    public IEnumerable<Sim.Domain.Entity.Parceiro> Parceiros { get; set; }
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
    public SelectList Secretarias { get; set; }

    private async Task OnLoad()
    {
        Secretarias = new SelectList(
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
<<<<<<< HEAD
                var sec = await _appServiceSecretaria.GetIdAsync(Input.Id);

                await _appServiceParceiro.AddAsync(
                    new EParceiro(){
                        Nome = Input.Nome,
                        Dominio = sec,
=======
                var sec = await _appServiceSecretaria.GetIdAsync(Input.Secretaria.Id);

                await _appServiceParceiro.AddAsync(
                    new Sim.Domain.Entity.Parceiro(){
                        Nome = Input.Nome,
                        Secretaria = sec,
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
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

