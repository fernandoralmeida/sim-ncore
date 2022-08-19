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

    public async Task<IActionResult> OnGetAsync(string ss, DateTime d, DateTime f
        , string? c, string? n, string? p, string? r, string? e, string? s, string? u){

        c = c?? "";
        n = n?? "";
        p = p?? "";
        r = r?? "";
        e = e?? "";
        s = s?? "";
        u = u?? "";
    
        var list = new List<ExportModel>();
        var _result = new List<Sim.Domain.Entity.Atendimento>();

        if(ss == "-") {   
            _result = (List<Sim.Domain.Entity.Atendimento>) 
                    await _appServiceAtendimento
                    .ListParamAsync(new List<object>() { d.ToShortDateString(), f.ToShortDateString(), c, n, p, r, e, s, u });
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

        var _file = await new Functions.ExportFile().ToExcel(list, $"lista-atend-{User.Identity.Name}-{DateTime.Now:yyyyMMddHHmmss}");

        return File(_file.StremFile, _file.ContentType, _file.Name);

    }

}