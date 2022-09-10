using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Cnpj.Interfaces;
using OfficeOpenXml;
using Microsoft.AspNetCore.Authorization;

namespace Sim.UI.Web.Areas.SimBI.Pages.Empresas
{
    [Authorize]
    public class ExportModel : PageModel
    {
        private readonly IAppServiceCnpj _appEmpresa;

        public ExportModel(IAppServiceCnpj appEmpresa)
        {
            _appEmpresa = appEmpresa;
        }
        public async Task<IActionResult> OnGetAsync(string ci, string cf, string m, string a)
        {
            var stream = new MemoryStream();

            var list = new List<InputExport>();
            var cont = 1;

            foreach ((string Cnpj, string RazaoSocial, string Tel, string Email, string Cnae) in await _appEmpresa.DoListCnaeEmpresasJsonAsync(ci, cf, m, a))
            {
                list.Add(new()
                {
                    N = cont++,
                    Cnpj = Cnpj,
                    Empresa = RazaoSocial,
                    Telefone = Tel,
                    Email = Email,
                    Atividade = Cnae
                });
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var epackage = new ExcelPackage(stream);
            var worksheet = epackage.Workbook.Worksheets.Add("Lista");
            worksheet.Cells.LoadFromCollection(list, true);
            await epackage.SaveAsync();

            stream.Position = 0;
            string excelname = $"lista-emp-{User.Identity.Name}-{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return File(stream, "application/vnd.openxmlformat-officedocument.spreadsheetml.sheet", excelname);
        }
    }
}
