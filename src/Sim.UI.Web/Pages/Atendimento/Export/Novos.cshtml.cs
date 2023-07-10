using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.Atendimento.Export;

[Authorize]
public class NovosModel : PageModel
{
    private readonly IAppServiceAtendimento _atendimentos;

    public NovosModel(IAppServiceAtendimento appServiceAtendimento)
    {
        _atendimentos = appServiceAtendimento;
    }

    public async Task<IActionResult> OnGetAsync(string src, DateTime? d1, DateTime? d2, string cpf,
    string nome, string cnpj, string rsocial, string cnae, string svc, string user, string sto)
    {

        var list = new List<ExportModel>();
        var singlelist = new List<ExportModel>();
        var _result = new List<EAtendimento>();

        if (string.IsNullOrEmpty(src))
        {

            cpf = cpf ?? "";
            nome = nome ?? "";
            cnpj = cnpj ?? "";
            rsocial = rsocial ?? "";
            cnae = cnae ?? "";
            svc = svc ?? "";
            user = user ?? "";
            sto = sto ?? "";

            _result = (List<EAtendimento>)
                    await _atendimentos
                    .ListParamAsync(new List<object>() { d1, d2, cpf, nome, cnpj, rsocial, cnae, svc, user, sto });
        }
        else
        {
            _result = (List<EAtendimento>)
                    await _atendimentos.DoListAendimentosAsyncBy(src);
        }

        var cont = 1;
        foreach (var at in _result)
        {

            var _pj = at.Empresa != null ? at.Empresa.Nome_Empresarial : "Anônimo";
            var _cliente = at.Pessoa != null ? at.Pessoa.Nome.Trim() : _pj;

            if (at.Pessoa != null)
            {
                if (at.Pessoa.Data_Cadastro.Value.Year == at.Data.Value.Year)
                    if (!list.Any(s => s.Cliente == _cliente) && _cliente != "Anônimo")
                        list.Add(new ExportModel
                        {
                            N = cont++,
                            Data = $"{at.Data.Value:yyyy-MM-dd}",
                            Inicio = $"{at.Data.Value:HH:mm}",
                            Termino = $"{at.DataF.Value:HH:mm}",
                            Cliente = _cliente,
                            Empresa = at.Empresa != null ? at.Empresa.CNPJ : "",
                            CNAE = at.Empresa != null ? at.Empresa.CNAE_Principal : "",
                            Atividade = at.Empresa != null ? at.Empresa.Atividade_Principal : "",
                            Logradouro_PF = at.Pessoa != null ? at.Pessoa.Logradouro : "",
                            Bairro_PF = at.Pessoa != null ? at.Pessoa.Bairro : "",
                            Municipio_PF = at.Pessoa != null ? at.Pessoa.Cidade : "",
                            Logradouro_PJ = at.Empresa != null ? at.Empresa.Logradouro : "",
                            Bairro_PJ = at.Empresa != null ? at.Empresa.Bairro : "",
                            Municipio_PJ = at.Empresa != null ? at.Empresa.Municipio : "",
                            Contato = at.Pessoa != null ? at.Pessoa.Tel_Movel : "",
                            Servico = at.Servicos != null ? at.Servicos : "",
                            Descricao = at.Descricao != null ? at.Descricao : "",
                            Setor = at.Setor != null ? at.Setor : "",
                            Canal = at.Canal != null ? at.Canal : "",
                            Atendente = at.Owner_AppUser_Id
                        });
            }

        }

        var _file = await new Functions.ExportFile().ToExcel(list, $"lista-atend-single-{User.Identity.Name}-{DateTime.Now:yyyyMMddHHmmss}");

        return File(_file.StremFile, _file.ContentType, _file.Name);

    }
}