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

        public async Task<IActionResult> OnPostExport()
        {
            var stream = new MemoryStream();
            var t = Task.Run(async () =>
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

                var list = new List<ExportModel>();
                var cont = 1;
                foreach (var e in await _appServiceAtendimento.ListParamAsync(param))
                {
                    if (e.Empresa != null)
                        list.Add(new ExportModel
                        {
                            N = cont++,
                            Data = e.Data.Value.ToString("MMMyyyy"),
                            Cliente = e.Pessoa.Nome,
                            Empresa = e.Empresa.CNPJ,
                            Atividade = e.Empresa.Atividade_Principal,
                            Contato = e.Pessoa.Tel_Movel,
                            Servico = e.Servicos,
                            Descricao = e.Descricao,
                            Setor = e.Setor
                        });
                    else
                        list.Add(new ExportModel
                        {
                            N = cont++,
                            Data = e.Data.Value.ToString("MMMyyyy"),
                            Cliente = e.Pessoa.Nome,
                            Empresa = "",
                            Atividade = "",
                            Contato = e.Pessoa.Tel_Movel,
                            Servico = e.Servicos,
                            Descricao = e.Descricao,
                            Setor = e.Setor
                        });
                }
                return list;
            });

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var epackage = new ExcelPackage(stream);
            var worksheet = epackage.Workbook.Worksheets.Add("Lista");
            worksheet.Cells.LoadFromCollection(await t, true);
            await epackage.SaveAsync();

            stream.Position = 0;
            string excelname = $"lista-atend-{User.Identity.Name}-{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return File(stream, "application/vnd.openxmlformat-officedocument.spreadsheetml.sheet", excelname);
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

                var lista = await _appServiceAtendimento.ListParamAsync(param);

                Input.ListaAtendimento = lista.ToList();
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
