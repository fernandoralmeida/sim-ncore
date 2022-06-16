using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Domain.Entity;
using Sim.Application.Interfaces;
using Sim.UI.Web.Functions;

namespace Sim.UI.Web.Pages.Agenda.Inscricoes.Novo
{


    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceInscricao _appServiceInscricao;
        private readonly IAppServiceEvento _appServiceEvento;
        private readonly IAppServicePessoa _appServicePessoa;
        private readonly IAppServiceEmpresa _appServiceEmpresa;

        public IndexModel(IAppServiceInscricao appServiceInscricao,
            IAppServiceEvento appServiceEvento,
            IAppServiceEmpresa appServiceEmpresa,
            IAppServicePessoa appServicePessoa)
        {
            _appServiceEvento = appServiceEvento;
            _appServiceInscricao = appServiceInscricao;
            _appServiceEmpresa = appServiceEmpresa;
            _appServicePessoa = appServicePessoa;
        }

        [BindProperty]
        public string GetCPF { get; set; }

        [BindProperty]
        public string GetCNPJ { get; set; }
        [BindProperty]
        public int GetNumeroEvento { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelInscricao Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public ICollection<Inscricao> ListaInscritos { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task LoadInscritos(int id)
        {
            var t = await _appServiceEvento.GetCodigoAsync(id);
            
            ListaInscritos = t.Inscritos.ToList();
            Input.Evento = t;
        }

        public async Task OnGetAsync(int? id)
        {
            if (id != null)
            {
                await LoadInscritos((int)id);
            }
            Input.Data_Inscricao = DateTime.Now;
        }

        public async Task OnPostIncluirPessoaAsync()
        {
            var t = await _appServicePessoa.ConsultaCPFAsync(GetCPF);
            Input.Participante = t.FirstOrDefault();
           

            if (Input.Participante != null)
            {
                var e = await _appServiceEmpresa.ConsultaRazaoSocialAsync(Input.Participante.CPF.MaskRemove());
                Input.Empresa = e.FirstOrDefault();
            }
        }

        public void OnPostRemoverPessoa()
        {
            Input.Participante = null;
        }

        public async Task OnPostIncluirEmpresaAsync()
        {

            var t = await _appServiceEmpresa.ConsultaCNPJAsync(GetCNPJ);
            Input.Empresa = t.FirstOrDefault();

        }

        public void OnPostRemoverEmpresa()
        {
            Input.Empresa = null;
        }

        public async Task OnPostIncluirEventoAsync()
        {
            try
            {
                var t = await _appServiceEvento.GetCodigoAsync(GetNumeroEvento);
                Input.Evento = t;
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
        }

        public void OnPostRemoverEvento()
        {
            Input.Evento = null;
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            var ja_inscricao = false;

            if (Input.Evento == null || Input.Participante == null)
            {
                StatusMessage = "Erro: Verifique se os campos foram preenchidos corretamente!";
                return Page();
            }

            var inscricao = new Inscricao()
            {
                AplicationUser_Id = User.Identity.Name,
                Data_Inscricao = DateTime.Now,
                Presente = false
            };

            var numero = _appServiceInscricao.LastCodigo();

            if (numero < 1)
                inscricao.Numero = 100001;
            else
                inscricao.Numero = numero + 1;

            var pessoa = await _appServicePessoa.GetIdAsync(Input.Participante.Id);

            if (pessoa != null)
                inscricao.Participante = pessoa;

            if (Input.Empresa != null)
            {
                var empresa = await _appServiceEmpresa.GetIdAsync(Input.Empresa.Id);

                if (empresa != null)
                    inscricao.Empresa = empresa;
            }

            var evento = await _appServiceEvento.GetIdAsync(Input.Evento.Id);

            if (evento != null)
                inscricao.Evento = evento;

            ja_inscricao = _appServiceInscricao.JaInscrito(inscricao.Participante.CPF, inscricao.Evento.Codigo);

            if (ja_inscricao)
                StatusMessage = "Erro: Pessoa já consta na lista de participantes!";
            
            else
                await _appServiceInscricao.AddAsync(inscricao);


            if (ja_inscricao)
                return Page();
            else
                return RedirectToPage("/Agenda/Index");

        }
    }
}
