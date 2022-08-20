using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Pages.Empresa.Export;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IAppServiceEmpresa _appServiceEmpresa;

    public IndexModel(IAppServiceEmpresa appServiceEmpresa){
        _appServiceEmpresa = appServiceEmpresa;
    }

    public async Task<IActionResult> OnGetAsync(string ss, string? c,
        string? r, string? e, string? l, string? b){

        c = c?? "";
        e = e?? "";
        l = l?? "";
        b = b?? "";
        r = r?? "";
    
        var list = new List<InputExport>();
        
        var _result = new List<Sim.Domain.Entity.Empresas>();

        if(ss == "-") {   
            _result = (List<Sim.Domain.Entity.Empresas>) 
                    await _appServiceEmpresa
                    .ListEmpresasAsync(new List<object>() { c, r, e, l, b });
        }
        else {            
            _result = (List<Sim.Domain.Entity.Empresas>)
                    await _appServiceEmpresa.DoListAsyncBy(ss);
        }

        var cont = 1;
        foreach (var ep in _result)
        {
            list.Add(new InputExport
            {
                N = cont++,
                Ano = ep.Data_Abertura.Value.Year,
                Cnpj = ep.CNPJ,
                Empresa = ep.Nome_Empresarial,
                Telefone = ep.Telefone,
                Email = ep.Email,
                Situacao = ep.Situacao_Cadastral,
                Endereco = string.Format("{0}, {1}", ep.Logradouro, ep.Numero),
                Municipio = ep.Municipio,
                Atividade = string.Format("{0} - {1}", ep.CNAE_Principal, ep.Atividade_Principal)
            });
        }

        var _file = await new Functions.ExportFile().ToExcel(list, $"lista-atend-{User.Identity.Name}-{DateTime.Now:yyyyMMddHHmmss}");

        return File(_file.StremFile, _file.ContentType, _file.Name);

    }

}