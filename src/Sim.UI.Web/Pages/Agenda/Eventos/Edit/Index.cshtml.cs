using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.Agenda.Eventos.Edit
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceTipo _appServiceTipo;
        private readonly IAppServiceEvento _appServiceEvento;
        private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceParceiro _appServiceParceiro;
        private readonly IMapper _mapper;

        public IndexModel(IAppServiceEvento appServiceEvento,
            IAppServiceTipo appServiceTipo,
            IAppServiceSetor appServiceSetor,
            IAppServiceParceiro appServiceParceiro,
            IMapper mapper)
        {
            _appServiceEvento = appServiceEvento;
            _appServiceTipo = appServiceTipo;
            _appServiceSetor = appServiceSetor;
            _appServiceParceiro = appServiceParceiro;
            _mapper = mapper;
        }

        [BindProperty(SupportsGet = true)]
        public InputModelEvento Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public SelectList TipoEventos { get; set; }
        public SelectList Setores { get; set; }

        public SelectList Parceiros { get; set; }
        public SelectList Situacoes { get; set; }

        private async Task Onload()
        {
            var t = await _appServiceTipo.ListAllAsync();

            var s = await _appServiceSetor.ListAllAsync();

            var p = await _appServiceParceiro.ListAllAsync();

            if (t != null)
            {
                TipoEventos = new SelectList(t, nameof(Tipo.Nome), nameof(Tipo.Nome), null);
            }

            if (s != null)
            {
                Setores = new SelectList(s, nameof(Setor.Nome), nameof(Setor.Nome), null);
            }

            if (p != null)
            {
                Parceiros = new SelectList(p, nameof(Parceiro.Nome), nameof(Parceiro.Nome), null);
            }
            Situacoes = new SelectList(Enum.GetNames(typeof(Evento.ESituacao)));
        }

        public async Task OnGet(Guid id)
        {
            await Onload();
            Input = _mapper.Map<InputModelEvento>(await _appServiceEvento.GetIdAsync(id));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    StatusMessage = "Verifique o preenchimento correto do formulário!";
                    await Onload();
                    return Page();
                }

                await _appServiceEvento.UpdateAsync(_mapper.Map<Evento>(Input));

                return RedirectToPage("/Agenda/Index");
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }
        }
    }
}
