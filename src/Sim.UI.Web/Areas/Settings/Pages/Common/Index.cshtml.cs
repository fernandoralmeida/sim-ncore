using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.VM;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Areas.Settings.Pages.Common
{
    public class IndexModel : PageModel
    {
<<<<<<< HEAD
        private readonly IAppServiceSecretaria _appServicePrefeitura;
        public IndexModel(IAppServiceSecretaria appService)
=======
        private readonly IAppServicePrefeitura _appServicePrefeitura;
        public IndexModel(IAppServicePrefeitura appService)
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
        {
            _appServicePrefeitura = appService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
<<<<<<< HEAD
        public VMSecretaria Input { get; set; }
        public IEnumerable<VMSecretaria> Listar { get; set; }
=======
        public VMPrefeitura Input { get; set; }
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
        
        private async Task OnLoad()
        {
            var t = await _appServicePrefeitura.DoList();
<<<<<<< HEAD
=======
            Input = new VMPrefeitura()
            {
                Listar = t.ToList(),
                Ativo = true
            };
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
        }

        public async Task OnGetAsync() => await OnLoad();

        public async Task OnPostAddAsync()
        {
<<<<<<< HEAD
=======
            if (ModelState.IsValid)
            {
                await _appServicePrefeitura.AddAsync(new EPrefeitura(new Guid(),Input.Nome, Input.Cidade, Input.UF, ativo: true));
                Input.Nome = string.Empty;
                Input.Cidade = string.Empty;
                Input.UF = string.Empty;
            }
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
            await OnLoad();
        }

        public async Task OnPostRemoveAsync(Guid id)
        {
            try
            {
<<<<<<< HEAD
                var sec = await _appServicePrefeitura.GetIdAsync(id);
=======
                var sec = await _appServicePrefeitura.GetByIdAsync(id);
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
                await _appServicePrefeitura.RemoveAsync(sec);

                await OnLoad();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar remover Prefeitura!" + "\n" + ex.Message;
            }
        }

        public async Task OnGetStatus(Guid id, bool st)
        {
            var _org = await _appServicePrefeitura.SingleIdAsync(id);
            _org.Ativo = st;
            await _appServicePrefeitura.UpdateAsync(_org);
        }
    }
}
