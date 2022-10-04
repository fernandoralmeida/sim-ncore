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

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Canal;

public class IndexModel : PageModel
{
<<<<<<< HEAD
    //private readonly IAppServiceSetor _appServiceSetor;
    private readonly IAppServiceSecretaria _appServiceSecretaria;
    private readonly IAppServiceCanal _appServiceCanal;
    public IndexModel(IAppServiceSecretaria appServiceSecretaria,
        IAppServiceCanal appServiceCanal)
    {
        //_appServiceSetor = appServiceSetor;
=======
    private readonly IAppServiceSetor _appServiceSetor;
    private readonly IAppServiceSecretaria _appServiceSecretaria;
    private readonly IAppServiceCanal _appServiceCanal;
    public IndexModel(IAppServiceSetor appServiceSetor,
        IAppServiceSecretaria appServiceSecretaria,
        IAppServiceCanal appServiceCanal)
    {
        _appServiceSetor = appServiceSetor;
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
        _appServiceSecretaria = appServiceSecretaria;
        _appServiceCanal = appServiceCanal;
    }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public VMCanal Input { get; set; }
<<<<<<< HEAD
    public VMSecretaria Organizacao { get; set; }

    public IEnumerable<ECanal> Canais { get; set; }
=======

    public IEnumerable<Sim.Domain.Entity.Canal> Canais { get; set; }
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
    public SelectList Setores { get; set; }
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

        Setores = new SelectList(
            await _appServiceSecretaria.ListAllAsync(),
<<<<<<< HEAD
            nameof(EOrganizacao.Id),
            nameof(EOrganizacao.Nome),
=======
            nameof(Sim.Domain.Entity.Setor.Id),
            nameof(Sim.Domain.Entity.Setor.Nome),
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
                var _sec = await _appServiceSecretaria.SingleIdAsync(Input.Id);
                //var set = await _appServiceSetor.SingleIdAsync(Input.Setor.Id);

                await _appServiceCanal.AddAsync(
                    new ECanal(){
                        Nome = Input.Nome,
                        Dominio = _sec,
=======
                var sec = await _appServiceSecretaria.SingleIdAsync(Input.Secretaria.Id);
                var set = await _appServiceSetor.SingleIdAsync(Input.Setor.Id);

                await _appServiceCanal.AddAsync(
                    new Sim.Domain.Entity.Canal(){
                        Nome = Input.Nome,
                        Secretaria = sec,
                        Setor = set,
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
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

