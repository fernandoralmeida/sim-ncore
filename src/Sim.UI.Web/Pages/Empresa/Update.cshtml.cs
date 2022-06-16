using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using AutoMapper;
using System.Text;

namespace Sim.UI.Web.Pages.Empresa
{
    using Sim.Application.SDE.Interface;
    using Sim.Domain.SDE.Entity;
    using Sim.Service.CNPJ.WebService;
    using Functions;

    [Authorize]
    public class UpdateModel : PageModel
    {
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IMapper _mapper;
        private readonly IReceitaWS _receitaWS;

        [BindProperty(SupportsGet = true)]
        public VMEmpresa Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        [DisplayName("Quadro Administrativo")]
        public string Lista { get; set; }

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

            StringBuilder sb = new StringBuilder();
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
                var t = Task.Run(() => _appServiceEmpresa.GetById(id));

                await t;

                Input = _mapper.Map<VMEmpresa>(t.Result);

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
                var cnpj = Input.CNPJ.MaskRemove();

                await LoadAsync(cnpj, Input.Id);

                var empresa = _mapper.Map<Empresas>(Input);

                _appServiceEmpresa.Update(empresa);
                
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
                { return Page(); }

                var t = Task.Run(() =>
                {

                    var empresa = _mapper.Map<Empresas>(Input);
                
                    _appServiceEmpresa.Update(empresa);

                });

                await t;

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
