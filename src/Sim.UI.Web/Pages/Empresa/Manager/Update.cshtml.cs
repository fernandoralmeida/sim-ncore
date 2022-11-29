using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.Text;
using AutoMapper;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using Sim.Application.WebService.RWS.Services;
using Sim.UI.Web.Functions;
using Sim.Application.VM;

namespace Sim.UI.Web.Pages.Empresa.Manager
{

    [Authorize]
    public class UpdateModel : PageModel
    {
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IMapper _mapper;
        private readonly IReceitaWS _receitaWS;

        [BindProperty(SupportsGet = false)]
        public VMEmpresa Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public UpdateModel(IAppServiceEmpresa appServiceEmpresa, IMapper mapper, IReceitaWS receitaWS)
        {
            _appServiceEmpresa = appServiceEmpresa;
            _mapper = mapper;
            _receitaWS = receitaWS;
        }

        private async Task LoadAsync(string cnpj, Guid id)
        {
            var rws = await _receitaWS.ConsultarCPNJAsync(cnpj);
            Input = _mapper.Map<VMEmpresa>(rws);

            foreach (var at in rws.AtividadePrincipal)
            {
                Input.CNAE_Principal = at.Code;
                Input.Atividade_Principal = at.Text;
            }

            StringBuilder sb = new();
            foreach (var at in rws.AtividadesSecundarias)
            {
                sb.AppendLine(string.Format("{0} - {1}", at.Code, at.Text));
            }

            Input.Atividade_Secundarias = sb.ToString().Trim();
            
            Input.Id = id;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                Input = _mapper.Map<VMEmpresa>(await _appServiceEmpresa.GetIdAsync(id));
                return Page();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return RedirectToPage("./Index");
            }
        }

        public async Task OnPostRWSAsync()
        {
            try
            {
                await LoadAsync(Input.CNPJ.MaskRemove(), Input.Id);
                await _appServiceEmpresa.UpdateAsync(_mapper.Map<Empresas>(Input));                
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;                
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();

                await _appServiceEmpresa.UpdateAsync(_mapper.Map<Empresas>(Input));

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }
        }
    }
}
