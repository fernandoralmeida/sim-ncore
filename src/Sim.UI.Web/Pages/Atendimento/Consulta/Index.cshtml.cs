using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

using Sim.Domain.Entity;
using Sim.Application.Interfaces;
using Sim.Identity.Interfaces;
using Sim.Identity.Entity;
using OfficeOpenXml;

namespace Sim.UI.Web.Pages.Atendimento.Consulta
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceServico _appServiceServico;
        private readonly IServiceUser _appIdentity;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Src { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public ParamModel GetParam { get; set; }

        public SelectList ListaAtendentes { get; set; }

        public SelectList ListaServicos { get; set; }

        public class InputModel
        {
            [DisplayName("Data Inicial")]
            [DataType(DataType.Date)]
            public DateTime? DataI { get; set; }

            [DisplayName("Data Final")]
            [DataType(DataType.Date)]
            public DateTime? DataF { get; set; }

            public string CPF { get; set; }

            public string CNPJ { get; set; }

            public string Nome { get; set; }

            [DisplayName("Razão Social")]
            public string RazaSocial { get; set; }

            public string CNAE { get; set; }

            public string Servico { get; set; }

            public string Atendente { get; set; }

            public ICollection<Domain.Entity.Atendimento> ListaAtendimento { get; set; }
        }

        public IndexModel(IAppServiceAtendimento appServiceAtendimento,
            IServiceUser appServiceUser,
            IAppServiceServico appServiceServico)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appIdentity = appServiceUser;
            _appServiceServico = appServiceServico;
            Input = new();
            GetParam = new();
        }

        public async Task OnGetAsync()
        {
            var lista = await _appServiceAtendimento.DoListAendimentosAsyncBy(Src);
            Input.ListaAtendimento = lista.ToList();
        }

        private async Task LoadUsers()
        {
            var t = await _appIdentity.ListAllAsync();

            if (t != null)
            {
                ListaAtendentes = new SelectList(t, nameof(ApplicationUser.UserName), nameof(ApplicationUser.UserName), null);
            }
        }

        private async Task LoadServicos()
        {
            var t = await _appServiceServico.ListAllAsync();

            if (t != null)
            {
                ListaServicos = new SelectList(t, nameof(Servico.Nome), nameof(Servico.Nome), null);
            }
        }

        public async Task OnPostAsync()
        {
            try
            {
                var lista = await _appServiceAtendimento.DoListAendimentosAsyncBy(Src);
                Input.ListaAtendimento = lista.ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                Input.ListaAtendimento = new List<Domain.Entity.Atendimento>();
            }
        }

        public async Task OnPostAtPendentesAsync()
        {
            try
            {
                Input.ListaAtendimento = _appServiceAtendimento.ListAtendimentoAtivoAsync(User.Identity.Name).Result.ToList(); 
                await LoadServicos();
                await LoadUsers();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                Input.ListaAtendimento = new List<Domain.Entity.Atendimento>();
            }
        }

        public async Task OnPostAtCanceladosAsync()
        {
            try
            {
                Input.ListaAtendimento = _appServiceAtendimento.ListAtendimentosCanceladosAsync(User.Identity.Name).Result.ToList();
                await LoadServicos();
                await LoadUsers();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                Input.ListaAtendimento = new List<Domain.Entity.Atendimento>();
            }

        }
    }
}
