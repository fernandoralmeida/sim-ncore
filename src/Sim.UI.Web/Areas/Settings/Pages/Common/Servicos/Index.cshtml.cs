using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.VM;
using Sim.Application.Interfaces;
using Sim.Domain.Organizacao.Model;
using Microsoft.AspNetCore.Authorization;

namespace Sim.UI.Web.Areas.Settings.Pages.Common.Servicos;

[Authorize(Roles = "Admin_Global,Admin_Config")]
public class IndexModel : PageModel
{
    private readonly IAppServiceServico _appservicos;
    private readonly IAppServiceSecretaria _appdominio;
    private readonly IMapper _mapper;

    public IndexModel(IAppServiceServico appservicos,
        IAppServiceSecretaria appdominio,
        IMapper mapper) {
            _appservicos = appservicos;
            _appdominio = appdominio;
            _mapper = mapper;        
    }

    [TempData]
    public string StatusMessage { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public VMServicos Input { get; set; }

    [BindProperty]
    public Guid ReturnID { get; set; }
    
    [BindProperty]
    public Guid SetorID { get; set; }
    [BindProperty]
    public Guid SetDominio { get; set; }
    public IEnumerable<EServico> Servicos { get; set; }
    public SelectList Dominios { get; set; }
    
    public async Task OnLoadAsync(Guid id, Guid dm) {
        var _list = await _appdominio.ListAllAsync();
        var _setores = await _appdominio.DoList(s => s.Id == id || s.Id == dm);
        Dominios = new SelectList(_setores, nameof(EOrganizacao.Id), nameof(EOrganizacao.Acronimo));
        ReturnID = dm;
        SetorID = id;
        Servicos = await _appservicos.DoListAsync(s => s.Dominio.Id == id || s.Dominio.Id == dm || s.Dominio == null);
    }

    public async Task OnGetAsync(string id, string dm) {
        await OnLoadAsync(new Guid(id), new Guid(dm));
    }

    public async Task OnPostAsync() {
        var _dominio = await _appdominio.SingleIdAsync(SetDominio);
        Input.Id= new Guid();
        Input.Ativo = true;
        Input.Dominio = _dominio;
        if (ModelState.IsValid) {}
            await _appservicos.AddAsync(_mapper.Map<EServico>(Input));
            await OnLoadAsync(SetorID, ReturnID);
            StatusMessage = "Novo serviço incluído com sucesso!";        
    }

    public async Task OnGetManager(string id, string dominio, string nome) {
        try {
            var _servico = await _appservicos.SingleIdAsync(new Guid(id));
            var _dominio = await _appdominio.SingleIdAsync(new Guid(dominio));
            _servico.Dominio = _dominio;
            _servico.Nome = nome;
            await _appservicos.UpdateAsync(_servico);
            StatusMessage = "Serviço alterado com sucesso!";
        }
        catch (Exception ex){
            StatusMessage = "Erro: " + ex.Message;
        }
    }
    public async Task OnGetRemove(Guid id, Guid dm, Guid st) {
        try
        {   
            var _serv = await _appservicos.SingleIdAsync(id);         
            await _appservicos.RemoveAsync(_serv);   
            await OnLoadAsync(st, dm);
        }
        catch (Exception ex)
        {
            StatusMessage = "Erro: " + ex.Message;
        }
    }
}

