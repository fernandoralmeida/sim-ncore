using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.VM;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Areas.Settings.Pages.Common
{
    public class IndexModel : PageModel
    {
        private readonly IAppServiceSecretaria _appServicePrefeitura;
        public IndexModel(IAppServiceSecretaria appService)
        {
            _appServicePrefeitura = appService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public VMSecretaria Input { get; set; }
        public IEnumerable<VMSecretaria> Listar { get; set; }
        
        private async Task OnLoad()
        {
            var t = await _appServicePrefeitura.DoList();
        }

        public async Task OnGetAsync() => await OnLoad();

        public async Task OnPostAddAsync()
        {
            await OnLoad();
        }

        public async Task OnPostRemoveAsync(Guid id)
        {
            try
            {
                var sec = await _appServicePrefeitura.GetIdAsync(id);
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
