using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Sim.Application.Interfaces;
using Sim.Domain.Organizacao.Model;
using AutoMapper;

namespace Sim.UI.Web.Pages.Atendimento.Novo
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceSecretaria _appSecretaria;
        private readonly IAppServiceCanal _appServiceCanal;
        private readonly IAppServiceServico _appServiceServico;
        private readonly IMapper _mapper;

        public IndexModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServiceCanal appServiceCanal,
            IAppServiceServico appServiceServico,
            IAppServiceSecretaria appSecretaria,
            IMapper mapper)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServiceCanal = appServiceCanal;
            _appServiceServico = appServiceServico;
            _appSecretaria = appSecretaria;
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
        public SelectList Canais { get; set; }
        public string GetServico { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ServicosSelecionados { get; set; }

        private async Task OnLoad()
        {
            var _org = await _appSecretaria.DoList(s => s.Hierarquia == EHierarquia.Secretaria);
            var _setores = await _appSecretaria.DoList(s => s.Hierarquia == EHierarquia.Setor && s.Dominio ==  _org.FirstOrDefault().Id);     
            var _canais = await _appServiceCanal.DoList(s => s.Dominio.Id == _org.FirstOrDefault().Id);
            
            Setores = new SelectList(_setores, nameof(EOrganizacao.Nome), nameof(EOrganizacao.Nome), null);
            Canais = new SelectList(_canais, nameof(ECanal.Nome), nameof(ECanal.Nome), null);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await OnLoad();

            var atendimemnto_ativio = await _appServiceAtendimento.ListAtendimentoAtivoAsync(User.Identity.Name);

            if (!atendimemnto_ativio.Any())
            {
                StatusMessage = "Alerta: Não existe atendimento ativo no momento!";
                return RedirectToPage("/Atendimento/Index");
            }

            Input = _mapper.Map<InputModelAtendimento>(atendimemnto_ativio.FirstOrDefault());

            return Page();
        }

        public async Task<JsonResult> OnGetCanais()
        {
            return new JsonResult(await _appServiceCanal.DoListJson(GetSetor));
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
