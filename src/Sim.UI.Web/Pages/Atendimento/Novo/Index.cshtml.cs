using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using AutoMapper;

namespace Sim.UI.Web.Pages.Atendimento.Novo
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceCanal _appServiceCanal;
        private readonly IAppServiceServico _appServiceServico;
        private readonly IMapper _mapper;

        public IndexModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServiceCanal appServiceCanal,
            IAppServiceServico appServiceServico,
            IAppServiceSetor appServiceSetor,
            IMapper mapper)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServiceCanal = appServiceCanal;
            _appServiceServico = appServiceServico;
            _appServiceSetor = appServiceSetor;
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

        public async Task<IActionResult> OnGetAsync()
        {
            await OnLoad();

            var atendimemnto_ativio = await _appServiceAtendimento.ListAtendimentoAtivoAsync(User.Identity.Name);

            if (!atendimemnto_ativio.Any())
            {
                StatusMessage = "Não existe atendimento ativo no momento!";
                return RedirectToPage("/Atendimento/Index");
            }

            Input = _mapper.Map<InputModelAtendimento>(atendimemnto_ativio.FirstOrDefault());

            return Page();
        }

        public JsonResult OnGetCanais()
        {
            return new JsonResult(_appServiceCanal.ListCanalOwner(GetSetor).Result);
        }

        public JsonResult OnGetServicos()
        {
            return new JsonResult(_appServiceServico.ListServicoOwnerAsync(GetSetor).Result);
        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {

                if (Input.Servicos == null || Input.Servicos == string.Empty)
                {
                    StatusMessage = "Erro: " + "Selecione um serviço ou mais!";
                    await OnLoad();
                    return RedirectToPage();
                }

                var atold = await _appServiceAtendimento.GetIdAsync(Input.Id);
                atold.DataF = DateTime.Now;
                atold.Setor = Input.Setor;
                atold.Canal = Input.Canal; 
                atold.Servicos = ServicosSelecionados; 
                atold.Descricao = Input.Descricao;
                atold.Status = "Finalizado";
                atold.Ultima_Alteracao = DateTime.Now;
                await _appServiceAtendimento.UpdateAsync(atold);

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
