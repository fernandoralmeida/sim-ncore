using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.Atendimento.Manager
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceCanal _appServiceCanal;
        private readonly IAppServiceServico _appServiceServico;

        public IndexModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServiceCanal appServiceCanal,
            IAppServiceServico appServiceServico,
            IAppServiceSetor appServiceSetor)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServiceCanal = appServiceCanal;
            _appServiceServico = appServiceServico;
            _appServiceSetor = appServiceSetor;
        }

        [BindProperty(SupportsGet = true)]
        public InputModelAtendimento Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [Required(ErrorMessage = "Selecione o setor do atendimento!")]
        [BindProperty(SupportsGet = true)]
        public string GetSetor { get; set; }

        public SelectList Setores { get; set; }
        public SelectList Canais { get; set; }
        public SelectList Servicos { get; set; }

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

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            await OnLoad();

            var atendimemnto_ativio = await _appServiceAtendimento.GetAtendimentoAsync((Guid)id);

            if(atendimemnto_ativio.Owner_AppUser_Id != User.Identity.Name)
            {
                StatusMessage = "Erro : Atendimento pertence a outro atendente!";
                return RedirectToPage("/Atendimento/Index");
            }

            if (atendimemnto_ativio == null)
            {
                StatusMessage = "Erro inesperado, tente novamente!";
                return RedirectToPage("/Atendimento/Index");
            }

            Input = new()
            {
                Id = atendimemnto_ativio.Id,
                Protocolo = atendimemnto_ativio.Protocolo,
                Data = atendimemnto_ativio.Data,
                DataF = atendimemnto_ativio.DataF,
                Setor = atendimemnto_ativio.Setor,
                Canal = atendimemnto_ativio.Canal,
                Servicos = atendimemnto_ativio.Servicos,
                Descricao = atendimemnto_ativio.Descricao,
                Status = atendimemnto_ativio.Status,
                Ultima_Alteracao = atendimemnto_ativio.Ultima_Alteracao,
                Ativo = atendimemnto_ativio.Ativo,
                Owner_AppUser_Id = atendimemnto_ativio.Owner_AppUser_Id,
                Pessoa = atendimemnto_ativio.Pessoa,
                Empresa = atendimemnto_ativio.Empresa                
            };

            var canal = await _appServiceCanal.ListCanalOwner(Input.Setor);

            if (canal != null)
            {
                Canais = new SelectList(canal, nameof(Canal.Nome), nameof(Canal.Nome), null);
            }

            var serv = await _appServiceServico.ListServicoOwnerAsync(Input.Setor);

            if (serv != null)
            {
                Servicos = new SelectList(serv, nameof(Servico.Nome), nameof(Servico.Nome), null);
            }
                        
            Input.Canal = atendimemnto_ativio.Canal;            
            Input.Servicos = atendimemnto_ativio.Servicos;
            ServicosSelecionados = atendimemnto_ativio.Servicos; 

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

        public async Task<IActionResult> OnPostAlterarAsync(Guid id)
        {

            try
            {

                var atold = await _appServiceAtendimento.GetIdAsync(id);
                atold.Setor = Input.Setor; 
                atold.Canal = Input.Canal; 

                if (Input.Servicos != null || Input.Servicos != string.Empty)
                    atold.Servicos = ServicosSelecionados; 
                else
                    atold.Servicos = Input.Servicos; 

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

        public async Task<IActionResult> OnPostExcluirAsync(Guid id)
        {

            try
            {
                var atold = await _appServiceAtendimento.GetIdAsync(id);
                await _appServiceAtendimento.RemoveAsync(atold);

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
