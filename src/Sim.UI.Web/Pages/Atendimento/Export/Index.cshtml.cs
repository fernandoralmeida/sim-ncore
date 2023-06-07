using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.Atendimento.Export;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IAppServiceAtendimento _appServiceAtendimento;

    public IndexModel(IAppServiceAtendimento appServiceAtendimento){
        _appServiceAtendimento = appServiceAtendimento;
    }

    public async Task<IActionResult> OnGetAsync(string src, DateTime? d1, DateTime? d2, string cpf,
        string nome, string cnpj, string rsocial, string cnae, string svc, string user, string setor){
    
        var list = new List<ExportModel>();
        var _result = new List<EAtendimento>();

        if(string.IsNullOrEmpty(src)) {  

            cpf = cpf ?? "";
            nome = nome ?? "";
            cnpj = cnpj ?? "";
            rsocial = rsocial ?? "";
            cnae =  cnae ?? "";
            svc = svc ?? "";
            user = user ?? "";
            setor = setor ?? "";

            _result = (List<EAtendimento>) 
                    await _appServiceAtendimento
                    .ListParamAsync(new List<object>() { d1, d2, cpf, nome, cnpj, rsocial, cnae, svc, user, setor });
        }
        else {            
            _result = (List<EAtendimento>)
                    await _appServiceAtendimento.DoListAendimentosAsyncBy(src);
        }

        var cont = 1;
        foreach (var at in _result){

            var _pj = at.Empresa != null ? at.Empresa.Nome_Empresarial: "Anônimo";
            var _cliente = at.Pessoa != null ? at.Pessoa.Nome : _pj;    

            list.Add(new ExportModel
            {
                N = cont++,
                Data = $"{at.Data.Value:yyyy-MM-dd}",
                Inicio = $"{at.Data.Value:HH:mm}",
                Termino = $"{at.DataF.Value:HH:mm}",
                Cliente = _cliente,
                Empresa = at.Empresa != null ? at.Empresa.CNPJ : "",
                Atividade = at.Empresa != null ? at.Empresa.Atividade_Principal : "", 
                Contato = at.Pessoa != null ? at.Pessoa.Tel_Movel: "",
                Servico = at.Servicos != null ? at.Servicos: "",
                Descricao = at.Descricao != null ? at.Descricao: "",
                Setor = at.Setor != null ? at.Setor: "",
                Canal = at.Canal != null ? at.Canal: "",
                Atendente = at.Owner_AppUser_Id
            });
        }

        var _file = await new Functions.ExportFile().ToExcel(list, $"lista-atend-{User.Identity.Name}-{DateTime.Now:yyyyMMddHHmmss}");

        return File(_file.StremFile, _file.ContentType, _file.Name);

    }

}