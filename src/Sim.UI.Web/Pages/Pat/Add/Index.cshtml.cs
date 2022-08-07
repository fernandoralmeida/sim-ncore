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
        public string InputSetor { get; set; }
        
        [BindProperty(SupportsGet = true)] 
        public string InputCanal { get; set; }   
        
        [BindProperty(SupportsGet = true)]
        public string InputServicos { get; set; }             
        
        public SelectList Setores { get; set; }
        public SelectList Canais { get; set; }
        public SelectList Servicos { get; set; }
        public string Descricao { get; set; }

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
            Setores = new SelectList(await _appServiceSetor.ListAllAsync(), 
                                    nameof(Setor.Nome), 
                                    nameof(Setor.Nome),
                                    null);
            Canais = new SelectList(await _appServiceCanal.ListAllAsync(),
                                    nameof(Canal.Nome),
                                    nameof(Canal.Nome),
                                    null);
            Servicos = new SelectList(await _appServiceServico.ListAllAsync(),
                                    nameof(Servico.Nome),
                                    nameof(Servico.Nome),
                                    null);
            InputSetor = "PAT";
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page(); 

            var emprego = new Empregos()
            {
                Empresa = await _appServiceEmpresa.GetIdAsync(Input.Empresa.Id),
                Data = Input.Data,
                Experiencia = Input.Experiencia,
                Vagas = Input.Vagas,
                Ocupacao = Input.Ocupacao,
                Pagamento = Input.Pagamento,
                Salario = Convert.ToDecimal(Input.Salario),
                Status = Input.Status
            };

            await _appServiceEmpregos.AddAsync(emprego);

            return RedirectToPage("/Pat/Index");
        }
    }
}
