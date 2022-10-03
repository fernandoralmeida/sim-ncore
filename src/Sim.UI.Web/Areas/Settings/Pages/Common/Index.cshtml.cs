using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Application.VM;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Areas.Settings.Pages.Common
{
    public class IndexModel : PageModel
    {
        private readonly IAppServicePrefeitura _appServicePrefeitura;
        public IndexModel(IAppServicePrefeitura appService)
        {
            _appServicePrefeitura = appService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public VMPrefeitura Input { get; set; }
        
        private async Task OnLoad()
        {
            var t = await _appServicePrefeitura.DoList();
            Input = new VMPrefeitura()
            {
                Listar = t.ToList(),
                Ativo = true
            };
        }

        public async Task OnGetAsync() => await OnLoad();

        public async Task OnPostAddAsync()
        {
            if (ModelState.IsValid)
            {
                await _appServicePrefeitura.AddAsync(new EPrefeitura(new Guid(),Input.Nome, Input.Cidade, Input.UF, ativo: true));
                Input.Nome = string.Empty;
                Input.Cidade = string.Empty;
                Input.UF = string.Empty;
            }
            await OnLoad();
        }

        public async Task OnPostRemoveAsync(Guid id)
        {
            try
            {
                var sec = await _appServicePrefeitura.GetByIdAsync(id);
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
