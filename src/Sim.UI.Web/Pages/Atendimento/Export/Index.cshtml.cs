using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Pages.Atendimento.Export;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IAppServiceAtendimento _appServiceAtendimento;

    public IndexModel(IAppServiceAtendimento appServiceAtendimento){
        _appServiceAtendimento = appServiceAtendimento;
    }

    public async Task<IActionResult> OnGetAsync(string ss, string? d, string? f
        , string? c, string? n, string? p, string? r, string? e, string? s, string? u){
        
        //var stream = new MemoryStream();
        var list = new List<ExportModel>();
        var _result = new List<Sim.Domain.Entity.Atendimento>();

        if(ss == "-") {   
            _result = (List<Sim.Domain.Entity.Atendimento>) 
                    await _appServiceAtendimento
                    .ListParamAsync(new List<object>() { d, f, c, n, p, r, e, s, u });
        }
        else {            
            _result = (List<Sim.Domain.Entity.Atendimento>)
                    await _appServiceAtendimento.DoListAendimentosAsyncBy(ss);
        }

        var cont = 1;
        foreach (var at in _result){
            list.Add(new ExportModel
            {
                N = cont++,
                Data = at.Data.Value.ToString("MMM-yyyy"),
                Cliente = at.Pessoa.Nome,
                Empresa = at.Empresa != null ? at.Empresa.CNPJ : "",
                Atividade = at.Empresa != null ? at.Empresa.Atividade_Principal : "", 
                Contato = at.Pessoa.Tel_Movel,
                Servico = at.Servicos,
                Descricao = at.Descricao,
                Setor = at.Setor
            });
        }

        //ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

        //using var epackage = new ExcelPackage(stream);
        //var worksheet = epackage.Workbook.Worksheets.Add("Lista");
        //worksheet.Cells.LoadFromCollection(list, true);
        //await epackage.SaveAsync();

        //stream.Position = 0;
        //string excelname = $"lista-atend-{User.Identity.Name}-{DateTime.Now:yyyyMMddHHmmss}.xlsx";

        //return File(stream, "application/vnd.openxmlformat-officedocument.spreadsheetml.sheet", excelname);
        var t = await new Functions.ExportFile().ToExcel(list, User.Identity.Name);
        return File(t.StremFile, t.ContentType, t.Name);
    }

}