using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Customer.Interfaces;
using Sim.Application.Interfaces;
using Sim.Domain.Customer.Models;
using Sim.Domain.Entity;
using Sim.UI.Web.Functions;

namespace Sim.UI.Web.Pages.Bind;

[Authorize(Roles = $"{Areas.Admin.Pages.Admin.Global},{Areas.Admin.Pages.Admin.Settings}")]
public class AdminModel : PageModel
{
    private readonly IAppServiceBindings _bindings;
    private readonly IAppServicePessoa _pessoas;
    private readonly IAppServiceEmpresa _empresa;

    [TempData]
    public string StatusMessage { get; set; }

    public IEnumerable<EBindings> Listar { get; set; }

    public IEnumerable<(string, int, TBindings)> Vinculos { get; set; }

    public IEnumerable<(string, int)> NotBindings { get; set; }

    public AdminModel(IAppServiceBindings bindings,
        IAppServiceEmpresa empresa,
        IAppServicePessoa pessoa)
    {
        _bindings = bindings;
        _empresa = empresa;
        _pessoas = pessoa;
    }

    public async Task OnGetAsync()
    {
        try
        {
            var _list = await _bindings.DoListAsync();
            var _vlist = new List<(string, int, TBindings)>();
            _vlist.Add(new(TBindings.Proprietario.ToString(), _list.Where(s => s.Vinculo == TBindings.Proprietario).Count(), TBindings.Proprietario));
            _vlist.Add(new(TBindings.Socio.ToString(), _list.Where(s => s.Vinculo == TBindings.Socio).Count(), TBindings.Socio));
            _vlist.Add(new(TBindings.Diretor.ToString(), _list.Where(s => s.Vinculo == TBindings.Diretor).Count(), TBindings.Diretor));
            _vlist.Add(new(TBindings.Gerente.ToString(), _list.Where(s => s.Vinculo == TBindings.Gerente).Count(), TBindings.Gerente));
            _vlist.Add(new(TBindings.Colaborador.ToString(), _list.Where(s => s.Vinculo == TBindings.Colaborador).Count(), TBindings.Colaborador));
            _vlist.Add(new(TBindings.Representante.ToString(), _list.Where(s => s.Vinculo == TBindings.Representante).Count(), TBindings.Representante));
            Vinculos = _vlist;
            var _e = await _empresa.DoList();
            var _p = await _pessoas.DoList();

            var _notbindigns = new List<(string, int)>();
            _notbindigns.Add(new("Pessoas", _p.Count()));
            _notbindigns.Add(new("Empresas", _e.Count()));

            NotBindings = _notbindigns;
        }
        catch (Exception ex)
        {
            StatusMessage = "Erro: " + ex.Message;
        }

    }

    public async Task OnPostPreviewAsync(TBindings id)
    {
        try
        {
            var _list = await _bindings.DoListAsync();
            var _vlist = new List<(string, int, TBindings)>();
            _vlist.Add(new(TBindings.Proprietario.ToString(), _list.Where(s => s.Vinculo == TBindings.Proprietario).Count(), TBindings.Proprietario));
            _vlist.Add(new(TBindings.Socio.ToString(), _list.Where(s => s.Vinculo == TBindings.Socio).Count(), TBindings.Socio));
            _vlist.Add(new(TBindings.Diretor.ToString(), _list.Where(s => s.Vinculo == TBindings.Diretor).Count(), TBindings.Diretor));
            _vlist.Add(new(TBindings.Gerente.ToString(), _list.Where(s => s.Vinculo == TBindings.Gerente).Count(), TBindings.Gerente));
            _vlist.Add(new(TBindings.Colaborador.ToString(), _list.Where(s => s.Vinculo == TBindings.Colaborador).Count(), TBindings.Colaborador));
            _vlist.Add(new(TBindings.Representante.ToString(), _list.Where(s => s.Vinculo == TBindings.Representante).Count(), TBindings.Representante));
            Vinculos = _vlist;
            Listar = _list.Where(s => s.Vinculo == id);
            var _e = await _empresa.DoListOnlyUnlinkeds();
            var _p = await _pessoas.DoListOnlyUnlinkeds();
        }
        catch (Exception ex)
        {
            StatusMessage = "Erro: " + ex.Message;
        }
    }

    public async Task OnPostBindingsAsync()
    {
        foreach (var p in await _pessoas.DoListOnlyUnlinkeds())        
            foreach (var e in await _empresa.DoList(s => s.Nome_Empresarial.Contains(p.CPF.MaskRemove()) && s.Situacao_Cadastral != "BAIXADA"))
            {
                var _validate = await _bindings.DoListAsync(s => s.Pessoa.CPF == p.CPF && s.Empresa.CNPJ == e.CNPJ);
                if (_validate.Count() < 1)                
                    await _bindings.AddAsync(new EBindings() { Empresa = e, Pessoa = p, Vinculo = TBindings.Proprietario });                
            }        

        var _list = await _bindings.DoListAsync();
        var _vlist = new List<(string, int, TBindings)>();
        _vlist.Add(new(TBindings.Proprietario.ToString(), _list.Where(s => s.Vinculo == TBindings.Proprietario).Count(), TBindings.Proprietario));
        _vlist.Add(new(TBindings.Socio.ToString(), _list.Where(s => s.Vinculo == TBindings.Socio).Count(), TBindings.Socio));
        _vlist.Add(new(TBindings.Diretor.ToString(), _list.Where(s => s.Vinculo == TBindings.Diretor).Count(), TBindings.Diretor));
        _vlist.Add(new(TBindings.Gerente.ToString(), _list.Where(s => s.Vinculo == TBindings.Gerente).Count(), TBindings.Gerente));
        _vlist.Add(new(TBindings.Colaborador.ToString(), _list.Where(s => s.Vinculo == TBindings.Colaborador).Count(), TBindings.Colaborador));
        _vlist.Add(new(TBindings.Representante.ToString(), _list.Where(s => s.Vinculo == TBindings.Representante).Count(), TBindings.Representante));
        Vinculos = _vlist;
    }
}