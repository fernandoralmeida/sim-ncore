using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.BancoPovo.Interfaces;
using Sim.Application.BancoPovo.ViewModel;
using Sim.Application.BancoPovo.Functions;
using Sim.Domain.BancoPovo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.BancoPovo.Recusados;

[Authorize(Roles = "Admin_Global,M_BancoPovo,M_BancoPovo_Admin")]
public class IndexModel : PageModel {

    private readonly IMapper _mapper;
    private readonly IAppServiceContratos _appcontratos;

    [BindProperty]
    public IEnumerable<VMContrato> MeusContratos { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public decimal TotalCredito { get; set; }

    [BindProperty]
    public Guid GetID { get; set; }

    [BindProperty]
    public EContrato.EnSituacao GetSituacao { get; set; }

    [BindProperty]
    public EContrato.EnPagamento GetPagamento { get; set; }

    public SelectList ESituacoes { get; set; }
    public SelectList EPagamentos { get; set; }

    public IndexModel (IMapper mapper,
        IAppServiceContratos appServiceContratos) {
            _mapper = mapper;
            _appcontratos = appServiceContratos;        
    }

    private void LoadSelectors() {
        ESituacoes = new SelectList(Enum.GetNames(typeof(EContrato.EnSituacao)));
        EPagamentos = new SelectList(Enum.GetNames(typeof(EContrato.EnPagamento)));
    }

    public async Task OnGetAsync() {     
        LoadSelectors();   
        var _list = await _appcontratos.DoListAsync(s => s.AppUser == User.Identity.Name && (s.Situacao == EContrato.EnSituacao.Reprovado || s.Situacao == EContrato.EnSituacao.Cancelado));      
        MeusContratos = _mapper.Map<IEnumerable<EContrato>, List<VMContrato>>(_list);
        TotalCredito= MeusContratos.Totalize();
        MeusContratos.OrderByDescending(o => o.Data);
    }

    public async Task OnPostAsync(string src) {
        var _list = await _appcontratos.DoListAsync(s => s.AppUser == User.Identity.Name && s.Situacao == EContrato.EnSituacao.Analise);
        var _list_src = _list.Where(s => s.Empresa.Nome_Empresarial.Contains(src) || s.Cliente.Nome.Contains(src));
        MeusContratos = _mapper.Map<IEnumerable<EContrato>,List<VMContrato>>(_list_src);
        MeusContratos.OrderByDescending(o => o.Data);
    }

    public async Task OnPostGoSituacaoAsync() {
        LoadSelectors();

        var _upsituacao = await _appcontratos.SingleIdAsync(GetID);

        _upsituacao.Situacao = GetSituacao;
        _upsituacao.DataSituacao = DateTime.Now;
        _upsituacao.Pagamento = EContrato.EnPagamento.Nulo;

        await _appcontratos.UpdateAsync(_upsituacao);
    }
}

