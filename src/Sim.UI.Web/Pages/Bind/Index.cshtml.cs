using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Domain.Customer.Models;
using Sim.Application.Customer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.UI.Web.Functions;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Pages.Bind;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IAppServiceBind _bind;
    private readonly IAppServicePessoa _pessoas;
    private readonly IAppServiceEmpresa _empresas;

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    public string GetCPF { get; set; }

    [BindProperty(SupportsGet = true)]
    public string GetCNPJ { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Nome { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid Id1 { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid Id2 { get; set; }

    [BindProperty(SupportsGet = true)]
    public string NomeEmpresarial { get; set; }
    public SelectList Vinculos { get; set; }
    public TBindings VinculoSelecionado { get; set; }

    public IEnumerable<EBind> Listar { get; set; }

    public IndexModel(IAppServiceBind repository,
        IAppServicePessoa pessoa,
        IAppServiceEmpresa empresa)
    {
        _bind = repository;
        _pessoas = pessoa;
        _empresas = empresa;
    }

    public async Task OnGetAsync()
        => await Task.Run(() =>
        {
            Vinculos = new SelectList(Enum.GetNames(typeof(TBindings)));
        });

    public async Task<JsonResult> OnGetAddPessoa(string doc)
    {
        var _result = new List<(Guid id,string document, string nome)>();

        var _con = doc.MaskRemove().Mask("###.###.###-##");
        foreach (var p in await _pessoas.ConsultaCPFAsync(_con))
        {
            _result.Add((p.Id, p.CPF, p.Nome));
        }

        return new JsonResult(_result);
    }

    public async Task<JsonResult> OnGetAddEmpresa(string doc)
    {
        var _result = new List<(Guid id,string document, string nome)>();

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
            Vinculos = new SelectList(Enum.GetNames(typeof(TBindings)));
            var _e = await _empresas.SingleIdAsync(Id2);
            var _p = await _pessoas.SingleIdAsync(Id1);
            //await _bonds.AddAsync(new EBonds(){Id = new Guid(), Empresa = _e, Representante = _p, Vinculo = VinculoSelecionado });
            StatusMessage = "Vinculo realizado com sucesso!";
            //var _v = await _bind.DoListAsync();
        });


}