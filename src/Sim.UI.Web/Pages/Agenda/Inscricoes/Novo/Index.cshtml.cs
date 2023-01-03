using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Domain.Entity;
using Sim.Application.Interfaces;
using Sim.UI.Web.Functions;
using System.ComponentModel;

namespace Sim.UI.Web.Pages.Agenda.Inscricoes.Novo
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceInscricao _appServiceInscricao;
        private readonly IAppServiceEvento _appServiceEvento;
        private readonly IAppServicePessoa _appServicePessoa;
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IAppServiceAtendimento _appatendimento;
        private readonly IAppServiceContador _appcontador;

        public IndexModel(IAppServiceInscricao appServiceInscricao,
            IAppServiceEvento appServiceEvento,
            IAppServiceEmpresa appServiceEmpresa,
            IAppServicePessoa appServicePessoa,
            IAppServiceAtendimento appServiceAtendimento,
            IAppServiceContador appServiceContador)
        {
            _appServiceEvento = appServiceEvento;
            _appServiceInscricao = appServiceInscricao;
            _appServiceEmpresa = appServiceEmpresa;
            _appServicePessoa = appServicePessoa;
            _appatendimento = appServiceAtendimento;
            _appcontador = appServiceContador;
        }

        [BindProperty]
        [DisplayName("CPF")]
        public string GetCPF { get; set; }

        [DisplayName("CNPJ")]
        [BindProperty]
        public string GetCNPJ { get; set; }
        [BindProperty]
        public int GetNumeroEvento { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelInscricao Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        private async Task<string> GetProtoloco()
        {        
            return await _appcontador.GetProtocoloAsync(User.Identity.Name, "Atendimento");
        }
        public async Task OnGetAsync(int? id)
        {
            
            if (id != null)
            {
                var _evento = await _appServiceEvento.DoListAsync(s => s.Codigo == (int)id);            
                Input.Evento = _evento.FirstOrDefault();            
            }
        }

        public async Task OnPostIncluirPessoaAsync()
        {
            var t = await _appServicePessoa.ConsultaCPFAsync(GetCPF);
            Input.Participante = t.FirstOrDefault();

            if (Input.Participante != null)
            {

                bool ja_inscricao = _appServiceInscricao.JaInscrito(GetCPF, Input.Evento.Codigo);

                if (ja_inscricao)
                {
                    StatusMessage = "Erro: Pessoa já consta na lista de participantes!";
                    Input.Participante = null;
                }
                else
                {

                    if (Input.Participante != null)
                    {
                        var e = await _appServiceEmpresa.ConsultaRazaoSocialAsync(Input.Participante.CPF.MaskRemove());
                        Input.Empresa = e.FirstOrDefault();
                    }
                }
            }
            else
                StatusMessage = "Alerta: Pessoa não encontrada no Sistema!";
        }

        public void OnPostRemoverPessoa()
        {
            Input.Participante = null;
        }

        public async Task OnPostIncluirEmpresaAsync()
        {
            var t = await _appServiceEmpresa.ConsultaCNPJAsync(GetCNPJ);
            Input.Empresa = t.FirstOrDefault();
            if (Input.Empresa == null)
                StatusMessage = "Alerta: Empresa não encontrada no Sistema!";
        }

        public void OnPostRemoverEmpresa()
        {
            Input.Empresa = null;
        }

        public async Task OnPostIncluirEventoAsync()
        {
            try
            {
                var _evento = await _appServiceEvento.DoListAsync(s => s.Codigo == GetNumeroEvento);
                Input.Evento = _evento.FirstOrDefault();
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
            try
            {
                if (Input.Evento == null || Input.Participante == null)
                {
                    StatusMessage = "Erro: Verifique se os campos foram preenchidos corretamente!";
                    return Page();
                }

                var inscricao = new Inscricao()
                {
                    AplicationUser_Id = User.Identity.Name,
                    Data_Inscricao = DateTime.Now,
                    Presente = false,
                    Numero = _appServiceInscricao.LastCodigo() < 1 ? 100001 : _appServiceInscricao.LastCodigo() + 1,
                    Participante = await _appServicePessoa.SingleIdAsync(Input.Participante.Id),
                    Empresa = Input.Empresa != null ? await _appServiceEmpresa.SingleIdAsync(Input.Empresa.Id) : null,
                    Evento = await _appServiceEvento.SingleIdAsync(Input.Evento.Id)
                };
                
                await _appServiceInscricao.AddAsync(inscricao);

                //var _setor = string.Empty;

                var _at = new EAtendimento(){
                    Protocolo = await GetProtoloco(),
                    Owner_AppUser_Id = User.Identity.Name,
                    Data = DateTime.Now,
                    DataF = DateTime.Now,
                    Pessoa = await _appServicePessoa.SingleIdAsync(Input.Participante.Id),
                    Empresa = Input.Empresa != null ? await _appServiceEmpresa.SingleIdAsync(Input.Empresa.Id) : null,
                    Setor = inscricao.Evento.Owner,
                    Servicos = "Inscrição em Evento",
                    Descricao =  string.Format("Incrição Nº:{0}, Evento: {1} {2}", inscricao.Numero, inscricao.Evento.Tipo, inscricao.Evento.Nome),
                    Canal = "Presencial",
                    Ativo = true,
                    Anonimo = false,
                    Status = "Finalizado",
                    Ultima_Alteracao = DateTime.Now
                };

                await _appatendimento.AddAsync(_at);

                return RedirectToPage("/Agenda/Inscricoes/Index", new { id = inscricao.Evento.Codigo });

            }
            catch(Exception ex)
            {
                StatusMessage = string.Format("Erro: {0}", ex.Message);
                return Page();
            }
        }
    }
}
