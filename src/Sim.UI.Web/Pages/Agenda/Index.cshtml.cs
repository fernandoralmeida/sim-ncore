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
            public IEnumerable<(string Mes, int Qtde, IEnumerable<Evento>)> ListaEventosMesFinalizados { get; set; }
            public IEnumerable<(string Mes, int Qtde, IEnumerable<Evento>)> ListaEventosMesCancelados { get; set; }
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
            var s = await _appServiceSetor.ListAllAsync();

            if (s != null)
                Setores = new SelectList(s, nameof(Setor.Nome), nameof(Setor.Nome), null);

            if (Input.Owner == null || Input.Owner == "Geral")
                Input.Owner = "";

            Tipos = new SelectList(await _appServiceTipo.ListAllAsync(), nameof(Tipo.Nome), nameof(Tipo.Nome), null);
        }

        public async Task OnGetAsync()
        {
            await Onload();

            Input.Ano = DateTime.Now.Year;

            var evento = await _appServiceEvento.ListAllAsync();

            Input.ListaEventosMes = await _appServiceEvento
                .ListEventosPorMesAsync(evento
                .Where(s => 
                s.Data.Value.Year == Input.Ano &&
                s.Situacao <= Evento.ESituacao.Ativo &&
                s.Owner.Contains(Input.Owner))
                .OrderBy(s => s.Data));

            Input.ListaEventosMesFinalizados = await _appServiceEvento
                .ListEventosPorMesAsync(evento
                .Where(s => 
                s.Data.Value.Year == Input.Ano &&
                s.Situacao == Evento.ESituacao.Finalizado &&
                s.Owner.Contains(Input.Owner))
                .OrderBy(s => s.Data));

            Input.ListaEventosMesCancelados = await _appServiceEvento
                .ListEventosPorMesAsync(evento
                .Where(s => 
                s.Data.Value.Year == Input.Ano &&
                s.Situacao == Evento.ESituacao.Cancelado &&
                s.Owner.Contains(Input.Owner))
                .OrderBy(s => s.Data));
        }

        public async Task OnPostEventAsync()
        {
            await Onload();

            var evento = await _appServiceEvento.ListNomeAsync(Input.Evento);

            Input.ListaEventosMes = await _appServiceEvento
                .ListEventosPorMesAsync(evento
                .Where(s => s.Data.Value.Year == Input.Ano && s.Situacao <= Evento.ESituacao.Ativo && s.Owner.Contains(Input.Owner)).OrderBy(s => s.Data));

            Input.ListaEventosMesFinalizados = await _appServiceEvento
                .ListEventosPorMesAsync(evento
                .Where(s => s.Data.Value.Year == Input.Ano && s.Situacao == Evento.ESituacao.Finalizado && s.Owner.Contains(Input.Owner)).OrderBy(s => s.Data));

            Input.ListaEventosMesCancelados = await _appServiceEvento
                .ListEventosPorMesAsync(evento
                .Where(s => s.Data.Value.Year == Input.Ano && s.Situacao == Evento.ESituacao.Cancelado && s.Owner.Contains(Input.Owner)).OrderBy(s => s.Data));
        }

        private int QuantosDiasFaltam(DateTime dataalvo)
        {
            return (int)dataalvo.Subtract(DateTime.Today).TotalDays;
        }
    }
}
