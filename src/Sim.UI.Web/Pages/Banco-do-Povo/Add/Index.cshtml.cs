using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.BancoPovo.Interfaces;
using Sim.Application.BancoPovo.ViewModel;
using Sim.Application.Interfaces;
using Sim.Domain.BancoPovo.Models;

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
    }
    public async Task OnPostPFAsync() {
        var lp = await _appServicePessoa.ConsultaCPFAsync(GetCPF);

        if(lp.Count() == 0)
            StatusMessage = "Erro: Cliente não encontrado!";

        foreach(var p in lp)
            InputContrato.Cliente = p;              
    }

    public async Task OnPostPJAsync() {
        var le = await _appServiceEmpresa.ConsultaCNPJAsync(GetCNPJ);

        if(le.Count() == 0)
            StatusMessage = "Erro: Empresa não encontrada!";

        foreach(var e in le)
            InputContrato.Empresa = e;
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
            
            await _appContratos.AddAsync(_mapper.Map<EContrato>(InputContrato));

            return RedirectToPage("/Banco-do-Povo/Index");
        }
        catch (Exception ex) {
            StatusMessage = "Erro: " + ex.Message;
            return Page();
        }
    }

}

