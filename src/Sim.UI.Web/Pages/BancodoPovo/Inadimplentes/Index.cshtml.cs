using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.BancoPovo.Interfaces;
using Sim.Application.BancoPovo.ViewModel;
using Sim.Application.BancoPovo.Functions;
using Sim.Domain.BancoPovo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.BancoPovo.Inadimplentes;

[Authorize(Roles = $"{Web.Areas.Admin.Pages.Admin.Global},SEDEMPI Banco do Povo")]
public class IndexModel : PageModel {

    private readonly IMapper _mapper;
    private readonly IAppServiceContratos _appcontratos;
    private readonly IAppServiceRenegociacoes _apprenegociar;

    [BindProperty]
    public IEnumerable<VMContrato> MeusContratos { get; set; }

    [BindProperty]
    public decimal TotalCredito { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public Guid GetID { get; set; }

    [BindProperty]
    public EContrato.EnSituacao GetSituacao { get; set; }

    [BindProperty]
    public EContrato.EnPagamento GetPagamento { get; set; }

    public SelectList ESituacoes { get; set; }
    public SelectList EPagamentos { get; set; }

    public IndexModel (IMapper mapper,
        IAppServiceRenegociacoes appServiceRenegociacoes,
        IAppServiceContratos appServiceContratos) {
            _mapper = mapper;
            _apprenegociar = appServiceRenegociacoes;
            _appcontratos = appServiceContratos;        
    }
    private void LoadSelectors() {
        ESituacoes = new SelectList(Enum.GetNames(typeof(EContrato.EnSituacao)));
        EPagamentos = new SelectList(Enum.GetNames(typeof(EContrato.EnPagamento)));
    }
    public async Task OnGetAsync() {      
        LoadSelectors();  
        var _list = await _appcontratos.DoListAsync(s => s.AppUser == User.Identity.Name && s.Situacao == EContrato.EnSituacao.Aprovado && s.Pagamento == EContrato.EnPagamento.Inadimplente);
        MeusContratos =  _mapper.Map<IEnumerable<EContrato>, List<VMContrato>>(_list);     
        TotalCredito = MeusContratos.Totalize();
        MeusContratos.OrderByDescending(o => o.Data);
    }

    public async Task OnPostAsync(string src) {
        var _list = await _appcontratos.DoListAsync(s => s.AppUser == User.Identity.Name && s.Situacao == EContrato.EnSituacao.Analise);
        var _list_src = _list.Where(s => s.Empresa.Nome_Empresarial.Contains(src) || s.Cliente.Nome.Contains(src));
        MeusContratos = _mapper.Map<IEnumerable<EContrato>, List<VMContrato>>(_list_src);
        MeusContratos.OrderByDescending(o => o.Data);
    }

    public async Task OnPostGoSituacaoAsync() {
        LoadSelectors();

        var _upsituacao = await _appcontratos.SingleIdAsync(GetID);

        _upsituacao.Situacao = GetSituacao;
        _upsituacao.Pagamento = GetPagamento;
        _upsituacao.DataSituacao = DateTime.Now;

        await _appcontratos.UpdateAsync(_upsituacao);
    }

    public async Task OnGetGoRenegociarAsync(Guid id) {
        
        var _upsituacao = await _appcontratos.SingleIdAsync(id);

        _upsituacao.Situacao = EContrato.EnSituacao.Aprovado;
        _upsituacao.Pagamento = EContrato.EnPagamento.Regular;
        _upsituacao.DataSituacao = DateTime.Now;

        var _renegociar = new ERenegociacoes(){
            Id = new Guid(),
            Contrato = _upsituacao,
            Data = DateTime.Now,
            Situacao = ERenegociacoes.EnSituacao.Ativo
        };       
        await _appcontratos.UpdateAsync(_upsituacao);
        await _apprenegociar.AddAsync(_renegociar);
    }
}

