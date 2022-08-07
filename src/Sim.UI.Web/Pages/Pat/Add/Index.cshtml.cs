using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            await LoadSelects();

            Input = new()
            {
                Data = DateTime.Now,
                Empresa = await _appServiceEmpresa.GetIdAsync(id),
                Salario = "0,00"
            };

            var at_list = await _appServiceAtendimento.ListAtendimentoAtivoAsync(User.Identity.Name);

            if (at_list.Any())
            {
                StatusMessage = "Um atendimento encontra-se ativo, finalize antes de iniciar outro atendimento.";                
                var atendimento = new Domain.Entity.Atendimento();
                foreach(var at in at_list)
                { 
                    atendimento = at;
                } 
                InputAtendimento.ID = atendimento.Id;               
            }
            else
            {
                var atendimento = new Domain.Entity.Atendimento()
                {                
                    Protocolo = await _appServiceContador.GetProtocoloAsync(User.Identity.Name, "Atendimento Empresa"),
                    Data = DateTime.Now,
                    Status = "Ativo",
                    Setor = InputAtendimento.InputSetor,
                    Ativo = true,
                    Empresa = await _appServiceEmpresa.GetIdAsync(Input.Empresa.Id),
                    Owner_AppUser_Id = User.Identity.Name
                };
                await _appServiceAtendimento.AddAsync(atendimento);
                var ret =  await _appServiceAtendimento.ListAtendimentoAtivoAsync(User.Identity.Name);
                InputAtendimento.ID = ret.SingleOrDefault().Id;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page(); 

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
                    Inclusivo = Input.Inclusiva,
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
