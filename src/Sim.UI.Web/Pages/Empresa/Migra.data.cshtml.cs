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
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace Sim.UI.Web.Pages.Empresa
{
    using Sim.Application.SDE.Interface;
    using Sim.Domain.SDE.Entity;
    using Sim.Service.CNPJ.WebService;
    using Sim.Domain.Cnpj.Interface;
    using Sim.Domain.Cnpj.Entity;

    [Authorize(Roles = "Administrador")]
    public class MigradataModel : PageModel
    {
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IMapper _mapper;
        private readonly IReceitaWS _receitaWS;
        private readonly ICNPJBase<BaseJucesp> _dbjucesp;
       

        public MigradataModel(IAppServiceEmpresa appServiceEmpresa, IMapper mapper, IReceitaWS receitaWS,
            ICNPJBase<BaseJucesp> dbjucesp)
        {
            _appServiceEmpresa = appServiceEmpresa;
            _mapper = mapper;
            _receitaWS = receitaWS;
            _dbjucesp = dbjucesp;
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


        private string Select_All
        {
            get
            {
                var sql = @"SELECT Campo1, Campo2, Campo3 FROM Estabelecimentos WHERE (Campo21 = '6607')";
                return sql;
            }
        }

        private async Task<IEnumerable<BaseJucesp>> LoadAsync()
        {
            var t = await _dbjucesp.ListAllAsync();           
            return t;
        }

        public void OnGet()
        {
            try
            {
                //var res = await LoadAsync();

                StatusMessage = string.Format("Iniciar Migração");
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
        }

        private async Task AddCNPJ(string cnpj)
        {

            var e = _appServiceEmpresa.ConsultaByCNPJ(cnpj);

           

                var rws = await _receitaWS.ConsultarCPNJAsync(cnpj);
                Input = _mapper.Map<VMEmpresa>(rws);

                if (rws.AtividadePrincipal != null)
                {

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
                        var empresa = _mapper.Map<Domain.SDE.Entity.Empresas>(Input);
                        _appServiceEmpresa.Add(empresa);
                    });
                    await t;
                }

        }

        public async Task<IActionResult> OnPostAsync()
        {

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var cont = 0;

            foreach(var emp in await LoadAsync())
            {
                cont++;
                await AddCNPJ(emp.CNPJ);

                System.Threading.Thread.Sleep(21000);
            }

            stopwatch.Stop();
                        
            StatusMessage2 = string.Format("Total: {0} | Tempo: {1}", cont, stopwatch.Elapsed);

            return Page();
        }

    }
}
