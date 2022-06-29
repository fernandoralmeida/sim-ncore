using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.Agenda
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEvento _appServiceEvento;
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceTipo _appServiceTipo;

        [BindProperty(SupportsGet = true)]
        public InputModelIndex Input { get; set; }

        public SelectList Setores { get; set; }

        public SelectList Tipos { get; set; }

        public class InputModelIndex
        {
            [DisplayName("Evento")]
            public string Evento { get; set; }
            public int Ano { get; set; }

            [DisplayName("Setor")]
            public string Owner { get; set; }
            public string Tipo { get; set; }
            public IEnumerable<InputModelEvento> ListaEventos { get; set; }
            public IEnumerable<(string Mes, int Qtde, IEnumerable<Evento>)> ListaEventosMes { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IndexModel(IAppServiceEvento appServiceEvento,
            IAppServiceSetor appServiceSetor,
            IAppServiceTipo appServiceTipo)
        {
            _appServiceEvento = appServiceEvento;
            _appServiceSetor = appServiceSetor;
            _appServiceTipo = appServiceTipo;
        }

        private async Task Onload()
        {
            Setores = new SelectList(
                await _appServiceSetor.ListAllAsync(),
                nameof(Setor.Nome), nameof(Setor.Nome), null);

            Tipos = new SelectList(
                await _appServiceTipo.ListAllAsync(),
                nameof(Tipo.Nome), nameof(Tipo.Nome), null);
        }

        public async Task OnGetAsync()
        {
            ViewData["ActivePageEvento"] = Agenda.AgendaNavPages.EventoAtivo;
            await Onload();

            Input.Ano = DateTime.Now.Year;

            Input.ListaEventosMes = await _appServiceEvento
                .ListEventosPorMesAsync(await _appServiceEvento.ListEventosAtivosAsync(Input.Ano));
        }

        public async Task OnPostEventAsync()
        {
            await Onload();
            Input.ListaEventosMes = await _appServiceEvento
                .ListEventosPorMesAsync(await _appServiceEvento.ListNomeAsync(Input.Evento));
        }

        public async Task OnPostAvailableAsync()
        {
            ViewData["ActivePageEvento"] = Agenda.AgendaNavPages.EventoAtivo;

            Input.ListaEventosMes = await _appServiceEvento
                .ListEventosPorMesAsync(await _appServiceEvento.ListEventosAtivosAsync(Input.Ano));
        }

        public async Task OnPostFinalizedAsync()
        {
            ViewData["ActivePageEvento"] = Agenda.AgendaNavPages.EventoFinalizado;

            Input.ListaEventosMes = await _appServiceEvento
                .ListEventosPorMesAsync(await _appServiceEvento.ListEventosFinalizadosAsync(Input.Ano));
        }

        public async Task OnPostCanceledAsync()
        {
            ViewData["ActivePageEvento"] = Agenda.AgendaNavPages.EventoCancelado;

            Input.ListaEventosMes = await _appServiceEvento
                .ListEventosPorMesAsync(await _appServiceEvento.ListEventosCanceladosAsync(Input.Ano));
        }

        private int QuantosDiasFaltam(DateTime dataalvo)
        {
            return (int)dataalvo.Subtract(DateTime.Today).TotalDays;
        }
    }
}
