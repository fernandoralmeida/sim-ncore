using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.Atendimento.Anonimo
{
    public class IndexModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceCanal _appServiceCanal;
        private readonly IAppServiceServico _appServiceServico;
        private readonly IAppServiceContador _appServiceContador;
        private readonly IMapper _mapper;

        public IndexModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServiceCanal appServiceCanal,
            IAppServiceServico appServiceServico,
            IAppServiceSetor appServiceSetor,
            IAppServiceContador appServiceContador,
            IMapper mapper)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServiceCanal = appServiceCanal;
            _appServiceServico = appServiceServico;
            _appServiceSetor = appServiceSetor;
            _appServiceContador = appServiceContador;
            _mapper = mapper;
        }

        [BindProperty(SupportsGet = true)]
        public InputModelAtendimento Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [Required(ErrorMessage = "Selecione o setor do atendimento!")]
        [BindProperty(SupportsGet = true)]
        public string GetSetor { get; set; }

        [BindProperty]
        public string GetCNPJ { get; set; }
        public SelectList Setores { get; set; }

        public string GetServico { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ServicosSelecionados { get; set; }
        
        private async Task OnLoad()
        {
            var set = await _appServiceSetor.ListAllAsync();           

            var lst = new List<Setor>();
            foreach (var s in set)
            {
                if (s.Nome != "Geral")
                    lst.Add(new Setor() { Nome = s.Nome, Secretaria = s.Secretaria, Id = s.Id, Ativo = s.Ativo, Canais = s.Canais, Servicos = s.Servicos });
            }

            if (lst != null)
            {
                Setores = new SelectList(lst, nameof(Setor.Nome), nameof(Setor.Nome), null);
            }
        }

        public async Task OnGetAsync()
        {
            await OnLoad();
        }

        public async Task<JsonResult> OnGetCanais()
        {
            return new JsonResult(await _appServiceCanal.ToListJson(GetSetor));
        }

        public async Task<JsonResult> OnGetServicos()
        {
            return new JsonResult(await _appServiceServico.ToListJson(GetSetor));
        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {

                if (Input.Servicos == null || Input.Servicos == string.Empty)
                {
                    StatusMessage = "Alerta: " + "Selecione um serviço ou mais!";
                    await OnLoad();
                    return RedirectToPage();
                }
    
                var _anonimo = new Domain.Entity.Atendimento()
                {
                    Protocolo = await _appServiceContador.GetProtocoloAsync(User.Identity.Name, "Atendimento Anônimo"),
                    Data = DateTime.Now,
                    DataF = DateTime.Now,
                    Status = "Finalizado",
                    Setor = Input.Setor,
                    Canal = Input.Canal,
                    Servicos = ServicosSelecionados,
                    Descricao = Input.Descricao,
                    Anonimo = true,
                    Ativo = true,
                    Ultima_Alteracao = DateTime.Now,
                    Owner_AppUser_Id = User.Identity.Name
                };

                await _appServiceAtendimento.AddAsync(_anonimo);

                return RedirectToPage("/Atendimento/Index");

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }

        }

    }
}