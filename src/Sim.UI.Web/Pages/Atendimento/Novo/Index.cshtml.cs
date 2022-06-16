using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sim.UI.Web.Pages.Atendimento.Novo
{
    using Sim.Application.SDE.Interface;
    using Sim.Application.Shared.Interface;
    using Sim.Domain.Shared.Entity;

    [Authorize]
    public class IndexModel : PageModel
    {
        //private readonly UserManager<ApplicationUser> _userManager;
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

        [BindProperty]
        public string GetCNPJ { get; set; }
        public SelectList Setores { get; set; }

        public string GetServico { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ServicosSelecionados { get; set; }

        private async Task OnLoad()
        {
            var set = Task.Run(() => _appServiceSetor.List());
            await set;

            var lst = new List<Setor>();
            foreach (var s in set.Result)
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

            //var user = await _userManager.GetUserAsync(User);

            var atendimemnto_ativio = _appServiceAtendimento.AtendimentoAtivo(User.Identity.Name).FirstOrDefault();

            if (atendimemnto_ativio == null)
            {
                StatusMessage = "Não existe atendimento ativo no momento!";
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

            return Page();
        }

        public JsonResult OnGetCanais()
        {
            return new JsonResult(_appServiceCanal.GetByOwner(GetSetor));
        }

        public JsonResult OnGetServicos()
        {
            return new JsonResult(_appServiceServico.GetByOwner(GetSetor));
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

                //var user = await _userManager.GetUserAsync(User);

                var t = Task.Run(() =>
                {
                    //var atendimemnto_ativio = _appServiceAtendimento.AtendimentoAtivo(user.Id).FirstOrDefault();

                    var atold = _appServiceAtendimento.GetById(Input.Id);
                    atold.DataF = DateTime.Now;
                    atold.Setor = Input.Setor; //GetSetor;
                    atold.Canal = Input.Canal;  //GetCanal;
                    atold.Servicos = ServicosSelecionados; //MeusServicos;
                    atold.Descricao = Input.Descricao;
                    atold.Status = "Finalizado";
                    atold.Ultima_Alteracao = DateTime.Now;
                    _appServiceAtendimento.Update(atold);

                });

                await t;

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
