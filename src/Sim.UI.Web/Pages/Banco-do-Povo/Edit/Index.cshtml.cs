using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.BancoPovo.Interfaces;
using Sim.Application.BancoPovo.ViewModel;
using Sim.Application.Interfaces;
using Sim.Domain.BancoPovo.Models;

namespace Sim.UI.Web.Pages.BancoPovo.Edit;

[Authorize(Roles = "Administrador,M_BancoPovo")]
public class IndexModel : PageModel
{   
    private readonly IMapper _mapper;
    private readonly IAppServiceEmpresa _appServiceEmpresa;
    private readonly IAppServicePessoa _appServicePessoa;
    private readonly IAppServiceContratos _appContratos;

    [TempData]
    public string StatusMessage { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public VMContrato InputContrato { get; set; }

    [DisplayName("CNPJ")]
    [BindProperty]
    public string GetCNPJ { get; set; }

    [DisplayName("CPF")]
    [BindProperty]
    public string GetCPF { get; set; }

    public SelectList ESituacoes { get; set; }

    public IndexModel(IMapper mapper,
        IAppServiceEmpresa appempresa,
        IAppServicePessoa apppessoa)
    {
        _mapper = mapper;
        _appServiceEmpresa = appempresa;
        _appServicePessoa = apppessoa;
    }

    public void OnGet() {
        InputContrato.Data = DateTime.Now;
        InputContrato.DataSituacao = DateTime.Now;
        InputContrato.Valor = 0;
        ESituacoes = new SelectList(Enum.GetNames(typeof(EContrato.EnSituacao)));
    }
    public async Task OnPostPFAsync() {
        ESituacoes = new SelectList(Enum.GetNames(typeof(EContrato.EnSituacao)));
        var lp = await _appServicePessoa.ConsultaCPFAsync(GetCPF);

        if(lp.Count() == 0)
            StatusMessage = "Erro: Cliente não encontrado!";

        foreach(var p in lp)
            InputContrato.Cliente = p;              
    }

    public async Task OnPostPJAsync() {
        ESituacoes = new SelectList(Enum.GetNames(typeof(EContrato.EnSituacao)));
        var le = await _appServiceEmpresa.ConsultaCNPJAsync(GetCNPJ);

        if(le.Count() == 0)
            StatusMessage = "Erro: Empresa não encontrada!";

        foreach(var e in le)
            InputContrato.Empresa = e;
    }

    public IActionResult OnPostRemovePF() {
        ESituacoes = new SelectList(Enum.GetNames(typeof(EContrato.EnSituacao)));
        InputContrato.Cliente = null;
        return RedirectToPage("/Banco-do-Povo/Add/Index");
    }

    public void OnPostRemovePJ() {
        ESituacoes = new SelectList(Enum.GetNames(typeof(EContrato.EnSituacao)));
        InputContrato.Empresa = null;
    }

    public async Task<IActionResult> OnPostSaveAsync(){
        try{
            ESituacoes = new SelectList(Enum.GetNames(typeof(EContrato.EnSituacao)));
            if (InputContrato.Cliente == null && InputContrato.Empresa == null)
            {
                StatusMessage = "Erro: Verifique se os campos foram preenchidos corretamente!";
                return Page();
            }
            
            var _contrato = _mapper.Map<EContrato>(InputContrato);
            _contrato.AppUser = User.Identity.Name;
            
            if(InputContrato.Cliente!= null)
                _contrato.Cliente = await _appServicePessoa.SingleIdAsync(InputContrato.Cliente.Id);
            
            if(InputContrato.Empresa!= null)
                _contrato.Empresa = await _appServiceEmpresa.SingleIdAsync(InputContrato.Empresa.Id);
            
            _contrato.Pagamento = EContrato.EnPagamento.Documentacao;

            _contrato.Valor = 0.0M;

            await _appContratos.AddAsync(_contrato);

            return RedirectToPage("/Banco-do-Povo/Index");
        }
        catch (Exception ex) {
            StatusMessage = "Erro: " + ex.Message + "\n" + ex.Data + "\n" + ex.Source + "\n" + ex.InnerException;
            return Page();
        }
    }

}

