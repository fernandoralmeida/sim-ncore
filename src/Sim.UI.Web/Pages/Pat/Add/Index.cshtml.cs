using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.UI.Web.Functions;

namespace Sim.UI.Web.Pages.Pat.Add
{
    [Authorize(Roles = "Administrador,M_Pat,Admin_Pat")]
    public class IndexModel : PageModel
    {   
        private readonly IMapper _mapper;
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceCanal _appServiceCanal;
        private readonly IAppServiceServico _appServiceServico;
        private readonly IAppServiceContador _appServiceContador;        
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IAppServiceEmpregos _appServiceEmpregos;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelAtendimento InputAtendimento { get; set; }
        public SelectList Setores { get; set; }
        public SelectList Canais { get; set; }
        public SelectList Servicos { get; set; }

        [BindProperty(SupportsGet = true)]
        public string InclusivasSelecionadas { get; set; }

        public IndexModel(IAppServiceEmpresa appServiceEmpresa,
            IAppServiceEmpregos appServiceEmpregos,
            IAppServiceAtendimento appServiceAtendimento,
            IAppServiceSetor appServiceSetor,
            IAppServiceCanal appServiceCanal,
            IAppServiceServico appServiceServico,
            IAppServiceContador appServiceContador,
            IMapper mapper) {        
            _appServiceEmpresa = appServiceEmpresa;
            _appServiceEmpregos = appServiceEmpregos;
            _appServiceAtendimento = appServiceAtendimento;
            _appServiceSetor = appServiceSetor;
            _appServiceCanal = appServiceCanal;
            _appServiceServico = appServiceServico;
            _appServiceContador = appServiceContador;
            _mapper = mapper;
        }

        private async Task LoadSelects()
        { 
            Setores = new SelectList(new List<string>(){ "PAT" });    
            InputAtendimento.InputSetor = "PAT";        
            Canais = new SelectList(await _appServiceCanal.ListAllAsync(),
                                    nameof(Canal.Nome),
                                    nameof(Canal.Nome),
                                    null);
            Servicos = new SelectList(await _appServiceServico.ListServicoOwnerAsync(InputAtendimento.InputSetor),
                                    nameof(Servico.Nome),
                                    nameof(Servico.Nome),
                                    null);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Input = new()
            {
                Data = DateTime.Now,
                Salario = "0,00"
            };
            await LoadSelects();

            var at = await _appServiceAtendimento.ListAtendimentoAtivoAsync(User.Identity.Name);

            if (at.Any())
            {
                StatusMessage = "Um atendimento encontra-se ativo, finalize antes de iniciar outro atendimento.";    
                return RedirectToPage("/Atendimento/Novo/Index");         
            }
            
            return Page();
        }

        public async Task<JsonResult> OnGetAddEmpresa(string cnpj){            
            return new JsonResult(await _appServiceEmpresa.ConsultaCNPJAsync(cnpj.Mask("##.###.###/####-##")));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    StatusMessage = "Alerta: Verifique se o formulário foi preenchido corretamente!";
                    return Page();
                }                     

                var _atendimento = await _appServiceAtendimento.GetIdAsync(InputAtendimento.ID);
                _atendimento.DataF = DateTime.Now;
                _atendimento.Setor = InputAtendimento.InputSetor;
                _atendimento.Canal = InputAtendimento.InputCanal; 
                _atendimento.Servicos = InputAtendimento.ServicosSelecionados; 
                _atendimento.Descricao = InputAtendimento.Descricao;
                _atendimento.Status = "Finalizado";
                _atendimento.Ultima_Alteracao = DateTime.Now;

                await _appServiceAtendimento.UpdateAsync(_atendimento);

                var emprego = new Empregos()
                {
                    Empresa = await _appServiceEmpresa.GetIdAsync(Input.Empresa.Id),
                    Data = Input.Data,
                    Experiencia = Input.Experiencia,
                    Inclusivo = InclusivasSelecionadas,
                    Vagas = Input.Vagas,
                    Ocupacao = Input.Ocupacao,
                    Pagamento = Input.Pagamento,
                    Salario = Convert.ToDecimal(Input.Salario),
                    Genero = Input.Genero,                    
                    Status = Input.Status
                };
                
                await _appServiceEmpregos.AddAsync(emprego);

                return RedirectToPage("/Pat/Index");
            }
            catch(Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }
        }
    }
}
