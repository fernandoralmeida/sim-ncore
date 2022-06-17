using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;


namespace Sim.UI.Web.Areas.Settings.Pages.Common
{
    public class ServicosModel : PageModel
    {
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceSecretaria _appServiceSecretaria;
        private readonly IAppServiceServico _appServiceServico;
        public ServicosModel(IAppServiceSetor appServiceSetor,
            IAppServiceSecretaria appServiceSecretaria,
            IAppServiceServico appServiceServico)
        {
            _appServiceSetor = appServiceSetor;
            _appServiceSecretaria = appServiceSecretaria;
            _appServiceServico = appServiceServico;
        }

        public class InputModel
        {
            [Key]
            [HiddenInput(DisplayValue = false)]
            public Guid Id { get; set; }

            //[Required]
            [DisplayName("Serviço")]
            public string Nome { get; set; }

            [DisplayName("Secretaria")]
            public Secretaria Secretaria { get; set; } //Secretaria

            [DisplayName("Setor")]
            public Setor Setor { get; set; } //Setor

            [DisplayName("Ativo")]
            public bool Ativo { get; set; }

            public virtual IEnumerable<Servico> Listar { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public Guid ItemSelecionado { get; set; }

        [BindProperty]
        public Guid SetorSelecionado { get; set; }

        public SelectList Secretarias { get; set; }

        public SelectList Setores { get; set; }

        private async Task OnLoad()
        {
            var sec = await _appServiceSecretaria.ListAllAsync();
            var set = await _appServiceSetor.ListAllAsync();
            var serv = await _appServiceServico.ListAllAsync();

            Input = new InputModel()
            {
                Listar = serv.ToList(),
                Ativo = true
            };

            if (sec != null)
                Secretarias = new SelectList(sec, nameof(Secretaria.Id), nameof(Secretaria.Nome), null);

            if (set != null)
                Setores = new SelectList(set, nameof(Setor.Id), nameof(Setor.Nome), null);          
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await OnLoad();
            return Page();
        }

        public async Task OnPostServicosBySetor()
        {
            await OnLoad();
            var setorname = await _appServiceSetor.GetIdAsync(SetorSelecionado);
            Input.Listar = await _appServiceServico.ListServicoOwnerAsync(setorname.Nome);
        }

        public async Task OnPostAddAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Input.Nome != null)
                    {

                        var sec = await _appServiceSecretaria.GetIdAsync(ItemSelecionado);
                        var set = await _appServiceSetor.GetIdAsync(SetorSelecionado);

                        var input = new Servico()
                        {
                            Nome = Input.Nome,
                            Secretaria = sec,
                            Setor = set,
                            Ativo = true
                        };

                        await _appServiceServico.AddAsync(input);
                    }
                }

                await OnLoad();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar incluír novo serviço!" + "\n" + ex.Message;
            }

        }

        public async Task OnPostRemoveAsync(Guid id)
        {
            try
            {
                var serv = await _appServiceServico.GetIdAsync(id);

                await _appServiceServico.RemoveAsync(serv);

                await OnLoad();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro ao tentar remover serviço!" + "\n" + ex.Message;
            }

        }
    }
}
