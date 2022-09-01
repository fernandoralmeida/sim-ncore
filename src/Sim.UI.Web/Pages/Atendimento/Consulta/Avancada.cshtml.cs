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

        public AvancadaModel(IAppServiceAtendimento appServiceAtendimento,
            IServiceUser appServiceUser,
            IAppServiceServico appServiceServico)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appIdentity = appServiceUser;
            _appServiceServico = appServiceServico;
            Input = new();
        }

        public async Task OnGetAsync()
        {
            Input.DataI = new DateTime(DateTime.Now.Year, 1, 1);
            Input.DataF = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Input.ListaAtendimento = new List<Domain.Entity.Atendimento>();
            await LoadUsers();
            await LoadServicos();
        }

        private async Task LoadUsers()
        {
            ListaAtendentes = new SelectList(await _appIdentity.ListAllAsync(), nameof(ApplicationUser.UserName), nameof(ApplicationUser.UserName), null);            
        }

        private async Task LoadServicos()
        {
            ListaServicos = new SelectList(await _appServiceServico.ListAllAsync(), nameof(Servico.Nome), nameof(Servico.Nome), null);           
        }

        public async Task OnPostAsync()
        {
            try
            {
            
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
    }
}
