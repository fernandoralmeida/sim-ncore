using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System.Text;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using Sim.Application.WebService.RWS.Services;
using Sim.UI.Web.Functions;

namespace Sim.UI.Web.Pages.Empresa.Novo
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IMapper _mapper;
        private readonly IReceitaWS _receitaWS;

        public IndexModel(IAppServiceEmpresa appServiceEmpresa, 
            IMapper mapper,
            IReceitaWS receitaWS)
        {
            _appServiceEmpresa = appServiceEmpresa;
            _mapper = mapper;
            _receitaWS = receitaWS;
        }

        [BindProperty(SupportsGet = true)]
        public VMEmpresa Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private async Task LoadAsync(string cnpj)
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

            await _appServiceEmpresa.AddAsync(_mapper.Map<Empresas>(Input));
            StatusMessage = "Empresa sincronizada com sucesso!";
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {
                if(!string.IsNullOrEmpty(id))
                    await LoadAsync(id);
                
                return Page();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return RedirectToPage();
            }                     
        }

        public async Task<IActionResult> OnPostRWSAsync()
        {
            var emp = await _appServiceEmpresa.ConsultaCNPJAsync(Input.CNPJ);
                        
            if(!emp.Any())              
                return RedirectToPage("/Empresa/Novo/Index", new { id = Input.CNPJ.MaskRemove() });
            
            else
                return RedirectToPage("/Empresa/Manager/Update", new { id = emp.FirstOrDefault().Id });
                       
        }
    }
}
