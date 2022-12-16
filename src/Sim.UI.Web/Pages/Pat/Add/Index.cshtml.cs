using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Application.Interfaces;
using Sim.Domain.Organizacao.Model;
using Sim.Domain.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.UI.Web.Functions;

namespace Sim.UI.Web.Pages.Pat.Add
{
    [Authorize(Roles = "Admin_Global,M_Pat,M_Pat_Admin")]
    public class IndexModel : PageModel
    {   
        private readonly IMapper _mapper;
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceSecretaria _appServiceSetor;
        private readonly IAppServiceCanal _appServiceCanal;
        private readonly IAppServiceServico _appServiceServico;
        private readonly IAppServiceContador _appServiceContador;        
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IAppServiceEmpregos _appServiceEmpregos;
        private readonly IAppServicePessoa _appServicePessoa;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public string InputSearch { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public Guid InputID { get; set; }

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
            IAppServiceSecretaria appServiceSetor,
            IAppServiceCanal appServiceCanal,
            IAppServiceServico appServiceServico,
            IAppServiceContador appServiceContador,
            IAppServicePessoa appServicePessoa,
            IMapper mapper) {        
            _appServiceEmpresa = appServiceEmpresa;
            _appServiceEmpregos = appServiceEmpregos;
            _appServiceAtendimento = appServiceAtendimento;
            _appServiceSetor = appServiceSetor;
            _appServiceCanal = appServiceCanal;
            _appServiceServico = appServiceServico;
            _appServiceContador = appServiceContador;
            _appServicePessoa = appServicePessoa;
            _mapper = mapper;
        }

        private async Task LoadSelects()
        { 
            Setores = new SelectList(new List<string>(){ "PAT" });    
            InputAtendimento.InputSetor = "PAT";        
            Canais = new SelectList(await _appServiceCanal.DoListAsync(),
                                    nameof(ECanal.Nome),
                                    nameof(ECanal.Nome),
                                    null);
            Servicos = new SelectList(await _appServiceServico.ListServicoOwnerAsync(InputAtendimento.InputSetor),
                                    nameof(EServico.Nome),
                                    nameof(EServico.Nome),
                                    null);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Input = new() {
                Data = DateTime.Now,
                Salario = "0,00"
            };
            await LoadSelects();

            var at = await _appServiceAtendimento.ListAtendimentoAtivoAsync(User.Identity.Name);

            if (at.Any()) {
                StatusMessage = "Um atendimento encontra-se ativo, finalize antes de iniciar outro atendimento.";    
                return RedirectToPage("/Atendimento/Novo/Index");         
            }
            
            return Page();
        }

        public async Task<JsonResult> OnGetAddEmpresa(string cnpj){ 
            var _result = new List<(Guid id, string doc, string nome, string tel, string email, string cnae)>();

            if(cnpj.MaskRemove().Length == 11)
            {
                var _con = cnpj.MaskRemove().Mask("###.###.###-##");
                foreach(var p in await _appServicePessoa.ConsultaCPFAsync(_con))
                {
                    _result.Add((p.Id, p.CPF, p.Nome, p.Tel_Movel, p.Email, p.Nome_Social));
                }
            }
            else if (cnpj.MaskRemove().Length == 14)
            {
                var _con = cnpj.MaskRemove().Mask("##.###.###/####-##");
                foreach(var e in await _appServiceEmpresa.ConsultaCNPJAsync(_con))
                {   
                    _result.Add((e.Id, e.CNPJ, e.Nome_Empresarial, e.Telefone, e.Email, e.Atividade_Principal));
                }
            }

            return new JsonResult(_result);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await LoadSelects();
                if(Input.Vagas <= 0) {
                    StatusMessage = "Alerta: A quantidade de vagas não pode ser menor ou igual a 0";
                    return Page();
                }

                if (!ModelState.IsValid) {
                    StatusMessage = "Alerta: Verifique se o formulário foi preenchido corretamente!";
                    return Page();
                } 

                var _atendimento = new EAtendimento();
                var _emprego = new Empregos();

                if(InputSearch.MaskRemove().Length == 11) {
                    var pess = await _appServicePessoa.SingleIdAsync(InputID);
                    _atendimento = new EAtendimento() {
                        Protocolo = await _appServiceContador.GetProtocoloAsync(User.Identity.Name, "Atendimento Empresa"),
                        Data = DateTime.Now,
                        DataF = DateTime.Now,
                        Setor = InputAtendimento.InputSetor,
                        Canal = InputAtendimento.InputCanal, 
                        Servicos = InputAtendimento.ServicosSelecionados, 
                        Descricao = InputAtendimento.Descricao,                
                        Status = "Finalizado",
                        Ultima_Alteracao = DateTime.Now,
                        Ativo = true,
                        Owner_AppUser_Id = User.Identity.Name,
                        Pessoa = pess,                
                        Anonimo = false
                    };
                    _emprego = new Empregos() {                    
                        Data = Input.Data,
                        Ocupacao = Input.Ocupacao,
                        Experiencia = Input.Experiencia,                    
                        Salario = Convert.ToDecimal(Input.Salario),
                        Vagas = Input.Vagas,
                        Pessoa = pess,
                        Pagamento = Input.Pagamento,
                        Status = Input.Status,                    
                        Genero = Input.Genero,                                       
                        Inclusivo = InclusivasSelecionadas
                    };
                }
                else {
                    var emp = await _appServiceEmpresa.SingleIdAsync(InputID);
                    _atendimento = new EAtendimento() {
                        Protocolo = await _appServiceContador.GetProtocoloAsync(User.Identity.Name, "Atendimento Empresa"),
                        Data = DateTime.Now,
                        DataF = DateTime.Now,
                        Setor = InputAtendimento.InputSetor,
                        Canal = InputAtendimento.InputCanal, 
                        Servicos = InputAtendimento.ServicosSelecionados, 
                        Descricao = InputAtendimento.Descricao,                
                        Status = "Finalizado",
                        Ultima_Alteracao = DateTime.Now,
                        Ativo = true,
                        Owner_AppUser_Id = User.Identity.Name,
                        Empresa = emp,                
                        Anonimo = false
                    };
                    _emprego = new Empregos() {                    
                        Data = Input.Data,
                        Ocupacao = Input.Ocupacao,
                        Experiencia = Input.Experiencia,                    
                        Salario = Convert.ToDecimal(Input.Salario),
                        Vagas = Input.Vagas,
                        Empresa = emp,
                        Pagamento = Input.Pagamento,
                        Status = Input.Status,                    
                        Genero = Input.Genero,                                       
                        Inclusivo = InclusivasSelecionadas
                    };
                } 

                await _appServiceAtendimento.AddAsync(_atendimento);                
                await _appServiceEmpregos.AddAsync(_emprego);

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
