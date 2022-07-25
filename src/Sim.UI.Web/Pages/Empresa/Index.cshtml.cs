using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Domain.Entity;
using Sim.Application.Interfaces;
using Sim.UI.Web.Functions;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;

namespace Sim.UI.Web.Pages.Empresa
{
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

        public Pagination<Empresas> PaginationEmpresas { get; set; }
        public int RegCount { get; set; }

        public class InputModel
        {
            
            [DisplayName("CNPJ")]
            public string CNPJ { get; set; }

            [DisplayName("Razao Social")]
            public string RazaoSocial { get; set; }
            public string CNAE { get; set; }
            public string Logradouro { get; set; }
            public string Bairro { get; set; }
        }

        public async Task OnGetAsync(string cnpj, string rs, string cnae, string lgd, string bro, int? pag)
        {
            try
            {
                    var param = new List<object>() {
                        cnpj,
                        rs,
                        cnae,
                        lgd,
                        bro
                    };

                    if (pag == null)
                        pag = 1;

                    var _list = await   _empresaApp.ListEmpresasAsync(param);
                    
                    RegCount = _list.Count();
                    
                    var pagesize = 10;

                    IQueryable<Empresas> _empresas = _list.AsQueryable();       

                    PaginationEmpresas = Pagination<Empresas>.Create(_empresas.AsNoTracking(), pag?? 1, pagesize);
            }
            catch(Exception ex)
            {
                StatusMessage ="Erro: " + ex.Message;
            }
        }

        public async Task<IActionResult> OnPostExport()
        {
            var stream = new MemoryStream();

            var param = new List<object>() {
                    Input.CNPJ,
                    Input.RazaoSocial,
                    Input.CNAE,
                    Input.Logradouro,
                    Input.Bairro
                };

            var list = new List<InputExport>();
            var cont = 1;

            foreach (var e in await _empresaApp.ListEmpresasAsync(param))
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

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var epackage = new ExcelPackage(stream);
            var worksheet = epackage.Workbook.Worksheets.Add("Lista");
            worksheet.Cells.LoadFromCollection(list, true);
            await epackage.SaveAsync();

            stream.Position = 0;
            string excelname = $"lista-emp-{User.Identity.Name}-{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return File(stream, "application/vnd.openxmlformat-officedocument.spreadsheetml.sheet", excelname);
        }

        public async Task OnPostAsync()
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
                        Input.Bairro
                    };

                    var _list = await   _empresaApp.ListEmpresasAsync(param);
                    
                    RegCount = _list.Count();
                    
                    var pagesize = 10;

                    IQueryable<Empresas> _empresas = _list.AsQueryable();       

                    PaginationEmpresas = Pagination<Empresas>.Create(_empresas.AsNoTracking(), 1, pagesize);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
        }
    }
}
