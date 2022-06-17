using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System.Text;
using System.Diagnostics;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using Sim.Application.WebService.RWS.Services;
using Sim.Domain.Cnpj.Entity;
using Sim.UI.Web.Functions;

namespace Sim.UI.Web.Pages.Empresa
{
    [Authorize(Roles = "Administrador")]
    public class MigradataModel : PageModel
    {
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IMapper _mapper;
        private readonly IReceitaWS _receitaWS;       

        public MigradataModel(IAppServiceEmpresa appServiceEmpresa,
            IMapper mapper,
            IReceitaWS receitaWS)
        {
            _appServiceEmpresa = appServiceEmpresa;
            _mapper = mapper;
            _receitaWS = receitaWS;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [TempData]
        public string StatusMessage1 { get; set; }

        [TempData]
        public string StatusMigration { get; set; }

        [TempData]
        public string StatusMessage2 { get; set; }

        [BindProperty(SupportsGet = true)]
        public VMEmpresa Input { get; set; }

        public void OnGet()
        {
            try
            {
                StatusMessage = string.Format("Iniciar Migração");
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
        }

        private async Task AddCNPJ(string cnpj)
        {
            var e = await _appServiceEmpresa.ConsultaCNPJAsync(cnpj);

            if (e.Any())
                return;

            var rws = await _receitaWS.ConsultarCPNJAsync(cnpj.MaskRemove());
            Input = _mapper.Map<VMEmpresa>(rws);

            if (rws.AtividadePrincipal != null)
            {

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
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var cont = 0;

            foreach(var emp in await _appServiceEmpresa.ListAllAsync())
            {
                cont++;
                await AddCNPJ(emp.CNPJ);
                Thread.Sleep(21000);
            }

            stopwatch.Stop();
                        
            StatusMessage2 = string.Format("Total: {0} | Tempo: {1}", cont, stopwatch.Elapsed);

            return Page();
        }

    }
}
