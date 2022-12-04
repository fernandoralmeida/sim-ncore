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
using Sim.UI.Web.Functions;

namespace Sim.UI.Web.Pages.BancoPovo.Add;

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
        IAppServicePessoa apppessoa,
        IAppServiceContratos appServiceContratos)
    {
        _mapper = mapper;
        _appServiceEmpresa = appempresa;
        _appServicePessoa = apppessoa;
        _appContratos = appServiceContratos;
    }

    public void OnGet() {
        InputContrato.Data = DateTime.Now;        
        InputContrato.Valor = 0;
        
    }
    public async Task OnPostPFAsync() {
        
        var lp = await _appServicePessoa.ConsultaCPFAsync(GetCPF);

        if(lp.Count() == 0)
            StatusMessage = "Erro: Cliente não encontrado!";

        foreach(var p in lp) {
            var _isOk = await _appContratos.DoListAsync(s => s.Cliente.CPF == GetCPF);

            if (_isOk.Count() > 0) {

                var _apto = _isOk.Where(s => s.Situacao > EContrato.EnSituacao.Aprovado &&
                                        s.Pagamento == EContrato.EnPagamento.Liquidado);

                if(_apto.Count() > 0) {
                    InputContrato.Cliente = p;
                    await CNPJ(p.CPF.MaskRemove());
                    return;
                }

                StatusMessage = "Erro: Cliente ja tem crédito ativo por essa instituição!";
                return;

            } else {

                InputContrato.Cliente = p;
                await CNPJ(p.CPF.MaskRemove());
                return;

            }              
        }          
    }

    private async Task CNPJ(string param) {
        var le = await _appServiceEmpresa.DoList(s => s.CNPJ == param || s.Nome_Empresarial.Contains(param));

        if(le.Count() == 0)
            StatusMessage = "Erro: Empresa não encontrada!";

        foreach(var p in le) {
            var _isOk = await _appContratos.DoListAsync(s => s.Empresa.CNPJ == param || s.Empresa.Nome_Empresarial.Contains(param));

            if (_isOk.Count() > 0) {

                var _apto = _isOk.Where(s => s.Situacao > EContrato.EnSituacao.Documentacao &&
                                        s.Pagamento == EContrato.EnPagamento.Liquidado);

                if(_apto.Count() > 0) {
                    InputContrato.Empresa = p;
                    return;
                }

                StatusMessage = "Erro: Empresa ja tem crédito ativo por essa instituição!";
                return;

            } else {

                InputContrato.Empresa = p;
                return;
            }              
        }  
    }
    public async Task OnPostPJAsync() {
        await CNPJ(GetCNPJ);
    }

    public IActionResult OnPostRemovePF() {
        
        InputContrato.Cliente = null;
        return RedirectToPage("/Banco-do-Povo/Add/Index");
    }

    public void OnPostRemovePJ() {
        
        InputContrato.Empresa = null;
    }

    public async Task<IActionResult> OnPostSaveAsync(){
        try{
            
            if (InputContrato.Cliente == null && InputContrato.Empresa == null)
            {
                StatusMessage = "Erro: Verifique se os campos foram preenchidos corretamente!";
                return Page();
            }
            
            var _contrato = _mapper.Map<EContrato>(InputContrato);
            _contrato.AppUser = User.Identity.Name;
            _contrato.Pagamento = EContrato.EnPagamento.Documentacao;
            _contrato.Situacao = EContrato.EnSituacao.Documentacao;
            _contrato.DataSituacao = DateTime.Now;
            _contrato.UltimaAlteracao = DateTime.Now;

            if(InputContrato.Cliente != null)
                _contrato.Cliente = await _appServicePessoa.SingleIdAsync(InputContrato.Cliente.Id);

            if(InputContrato.Empresa != null)
                _contrato.Empresa = await _appServiceEmpresa.SingleIdAsync(InputContrato.Empresa.Id);

            await _appContratos.AddAsync(_contrato);

            return RedirectToPage("/Banco-do-Povo/Index");
        }
        catch (Exception ex) {
            StatusMessage = "Erro: " + ex.Message + "\n" + ex.Data + "\n" + ex.Source + "\n" + ex.InnerException;
            return Page();
        }
    }

}

