using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Sim.UI.Web.Pages.Empresa
{
    using Sim.Domain.SDE.Entity;
    using Sim.Application.SDE.Interface;
    using Sim.Domain.Cnpj.Entity;
    using Functions;
    using OfficeOpenXml;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpresa  _empresaApp;

        public IndexModel(IAppServiceEmpresa appServiceEmpresa)
        {
            _empresaApp = appServiceEmpresa;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public SelectList Municipios { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public class InputModel
        {
            
            [DisplayName("CNPJ")]
            public string CNPJ { get; set; }

            [DisplayName("Razao Social")]
            public string RazaoSocial { get; set; }
            public string CNAE { get; set; }
            public string Logradouro { get; set; }
            public string Bairro { get; set; }
            public IEnumerable<Empresas> ListaEmpresas { get; set; }
        }

        public void OnGet()
        {        }

        public IActionResult OnPostExport()
        {
            var stream = new MemoryStream();
            var t = Task.Run(() =>
            {
                var param = new List<object>() {
                    Input.CNPJ,
                    Input.RazaoSocial,
                    Input.CNAE,
                    Input.Logradouro,
                    Input.Bairro
                };

                var list = new List<InputExport>();
                var cont = 1;
                foreach (var e in _empresaApp.ListByParam(param).Result)
                {
                    list.Add(new InputExport
                    {
                        N = cont++,
                        Ano = e.Data_Abertura.Value.Year,
                        Cnpj = e.CNPJ,
                        Empresa = e.Nome_Empresarial,
                        Telefone = e.Telefone,
                        Email = e.Email,
                        Situacao = e.Situacao_Cadastral,
                        Endereco = string.Format("{0}, {1}", e.Logradouro, e.Numero),
                        Municipio = e.Municipio,
                        Atividade = string.Format("{0} - {1}", e.CNAE_Principal, e.Atividade_Principal)
                    });
                }
                return list;
            });

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var epackage = new ExcelPackage(stream);
            var worksheet = epackage.Workbook.Worksheets.Add("Lista");
            worksheet.Cells.LoadFromCollection(t.Result, true);
            epackage.SaveAsync();

            stream.Position = 0;
            string excelname = $"lista-emp-{User.Identity.Name}-{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return File(stream, "application/vnd.openxmlformat-officedocument.spreadsheetml.sheet", excelname);
        }

        public async Task OnPostViewAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {                    
                    var param = new List<object>() {
                    Input.CNPJ,
                    Input.RazaoSocial,
                    Input.CNAE,
                    Input.Logradouro,
                    Input.Bairro};

                    Input.ListaEmpresas = await _empresaApp.ListByParam(param);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
        }
    }
}
