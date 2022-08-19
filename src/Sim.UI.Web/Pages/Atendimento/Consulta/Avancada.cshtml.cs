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
    public class AvancadaModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceServico _appServiceServico;
        private readonly IServiceUser _appIdentity;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public ParamModel GetParam { get; set; }

        public SelectList ListaAtendentes { get; set; }

        public SelectList ListaServicos { get; set; }

        public RouteID Route { get; set; }

        public class RouteID{
            public DateTime? datai{ get ; set; }
            public DateTime? dataf{ get ; set; }
            public string cpf{ get ; set; }
            public string nome{ get ; set; }
            public string cnpj{ get ; set; }
            public string razaosocial{ get ; set; }
            public string cnae{ get ; set; }
            public string servico{ get ; set; }
            public string atendente{ get ; set; }
        }

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

        public AvancadaModel(IAppServiceAtendimento appServiceAtendimento,
            IServiceUser appServiceUser,
            IAppServiceServico appServiceServico)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appIdentity = appServiceUser;
            _appServiceServico = appServiceServico;
            Input = new();
            Route = new();
            GetParam = new();
        }

        public void OnGet()
        {
            Input.DataI = new DateTime(DateTime.Now.Year, 1, 1);
            Input.DataF = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Input.ListaAtendimento = new List<Domain.Entity.Atendimento>();
            LoadUsers().Wait();
            LoadServicos().Wait();
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
                Route.datai = Input.DataI.Value;
                Route.dataf = Input.DataF.Value;
                Route.cpf = Input.CPF != null ? Input.CPF : "";
                Route.nome = Input.Nome != null ? Input.Nome : "";
                Route.cnpj = Input.CNPJ != null ? Input.CNPJ : "";
                Route.razaosocial = Input.RazaSocial != null ? Input.RazaSocial : "";
                Route.cnae = Input.CNAE != null ? Input.CNAE : "";
                Route.servico = Input.Servico != null ? Input.Servico : "";
                Route.atendente = Input.Atendente != null ? Input.Atendente : "";
             
                var param = new List<object>() {
                    Input.DataI.Value.Date,
                    Input.DataF.Value.Date,
                    Input.CPF,
                    Input.Nome,
                    Input.CNPJ,
                    Input.RazaSocial,
                    Input.CNAE,
                    Input.Servico,
                    Input.Atendente  };

                Input.ListaAtendimento = (ICollection<Sim.Domain.Entity.Atendimento>) await _appServiceAtendimento.ListParamAsync(param);
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                Input.ListaAtendimento = new List<Domain.Entity.Atendimento>();
            }

            await LoadServicos();
            await LoadUsers();
        }

        public async Task OnPostAtPendentesAsync()
        {
            try
            {
                Input.ListaAtendimento = (ICollection<Sim.Domain.Entity.Atendimento>) await _appServiceAtendimento.ListAtendimentoAtivoAsync(User.Identity.Name); 
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
                Input.ListaAtendimento = (ICollection<Sim.Domain.Entity.Atendimento>) await _appServiceAtendimento.ListAtendimentosCanceladosAsync(User.Identity.Name);
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
