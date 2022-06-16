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
    using Sim.Service.CNPJ.Entity;
    using Functions;

    [Authorize]
    public class AddModel : PageModel
    {
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IMapper _mapper;
        private readonly IReceitaWS _receitaWS;

        public AddModel(IAppServiceEmpresa appServiceEmpresa, IMapper mapper, IReceitaWS receitaWS)
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

            StringBuilder sb = new StringBuilder();
            foreach (var at in rws.AtividadesSecundarias)
            {
                sb.AppendLine(string.Format("{0} - {1}", at.Code, at.Text));
            }

            Input.Atividade_Secundarias = sb.ToString().Trim();

            var t = Task.Run(() =>
            {
                var empresa = _mapper.Map<Empresas>(Input);
                _appServiceEmpresa.Add(empresa);
                StatusMessage = "Empresa sincronizada com sucesso!";
            });
            await t;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {
                if(id != null)
                {
                    await LoadAsync(id);
                }
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
            var emp = Task.Run(() => _appServiceEmpresa.ConsultaByCNPJ(Input.CNPJ).FirstOrDefault());
            await emp;
            if(emp.Result == null)
            {
                var cnpj = Input.CNPJ.MaskRemove();
                
                return RedirectToPage("./Add", new { id = cnpj });
            }
            else
                return RedirectToPage("./Update", new { id = emp.Result.Id });
                       
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            { return Page(); }

            var t = Task.Run(() =>
            {                

                var empresa = _mapper.Map<Empresas>(Input);

                _appServiceEmpresa.Add(empresa);

            });
            await t;
            return RedirectToPage("./Index");
        }
    }
}
