using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Customer.Interfaces;
using Sim.Application.Interfaces;
using Sim.Application.Sebrae.Interfaces;
using Sim.Domain.Cnpj.Extensions;
using Sim.Domain.Customer.Models;
using Sim.Domain.Entity;
using Sim.Domain.Sebrae.Model;

namespace Sim.UI.Web.Pages.SebraeAqui.SimplesNacional;

[Authorize]
public class AddModel : PageModel
{
    private readonly IAppServiceSimples _simples;
    private readonly IAppServiceEmpresa _empresas;
    public bool _result = false;
    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    public Empresas InputEmpresa { get; set; }

    [BindProperty(SupportsGet = true)]
    public string InputDocumento { get; set; }

    [BindProperty(SupportsGet = true)]
    public string InputExercicio { get; set; }

    [BindProperty(SupportsGet = true)]
    public string InputChave { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    public IEnumerable<ESimples> Listar { get; set; }

    public AddModel(IAppServiceSimples simples,
        IAppServicePessoa pessoa,
        IAppServiceEmpresa empresa)
    {
        _simples = simples;
        _empresas = empresa;
    }

    public void OnGet() { }
    public async Task<JsonResult> OnGetAddEmpresa(string doc)
    {
        var _result = new List<(Guid id, string document, string nome)>();

        var _con = doc.MaskRemove().Mask("##.###.###/####-##");
        foreach (var p in await _empresas.ConsultaCNPJAsync(_con))
        {
            _result.Add((p.Id, p.CNPJ, p.Nome_Empresarial));
        }

        return new JsonResult(_result);
    }

    public async Task OnPostAsync()
        => await Task.Run(async () =>
        {
            var _e = await _empresas.SingleIdAsync(Id);

            foreach (var item in await _simples.DoListAsync(s => s.Empresa.Id == _e.Id))
            {
                StatusMessage = $"Alerta: Empresa já tem uma chave do Simples Cadastrada!";
                Listar = await _simples.DoListAsync(s => s.Empresa.Id == _e.Id);
                _result = true;
                return;
            }

            if (_e != null)
            {
                await _simples.AddAsync(new ESimples()
                {
                    Empresa = _e,
                    Documento = InputDocumento,
                    Exercicio = InputExercicio,
                    Chave = InputChave
                });
                Listar = await _simples.DoListAsync(s => s.Chave == InputChave);
                _result = true;
            }
            else
            {
                StatusMessage = $"Alerta: Empresa não encontrada!";
                _result = false;
                return;
            }
        });


}