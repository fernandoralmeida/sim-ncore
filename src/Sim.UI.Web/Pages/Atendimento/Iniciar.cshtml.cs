using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using Sim.UI.Web.Functions;

namespace Sim.UI.Web.Pages.Atendimento
{


    [Authorize]
    public class IniciarModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServicePessoa _appServicePessoa;
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        private readonly IAppServiceContador _appServiceContador;

        public IniciarModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServicePessoa appServicePessoa,
            IAppServiceEmpresa appServiceEmpresa,
            IAppServiceContador appServiceContador)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServicePessoa = appServicePessoa;
            _appServiceEmpresa = appServiceEmpresa;
            _appServiceContador = appServiceContador;
        }

        [DisplayName("CPF")]
        [BindProperty(SupportsGet = true)]
        public string GetCPF { get; set; }

        [DisplayName("CNPJ")]
        [BindProperty(SupportsGet = true)]
        public string GetCNPJ { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelAtendimento Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private async Task<string> GetProtoloco()
        {        
            return await _appServiceContador.GetProtocoloAsync(User.Identity.Name, "Atendimento");
        }
        private async Task<Pessoa> GetPessoa(Guid id)
        {
            return await _appServicePessoa.GetIdAsync(id);
        }
        private async Task<Empresas> GetEmpresa(string cnpj)
        {
            var emp = await _appServiceEmpresa.ConsultaCNPJAsync(cnpj);
            StatusMessage = emp.Any() ? string.Empty : "Erro: Empresa não cadastrada!";
            return emp.Any() ? emp.FirstOrDefault() : null;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            Input = new();
             
            foreach (var at in await _appServiceAtendimento.ListAtendimentoAtivoAsync(User.Identity.Name))
            {
                StatusMessage = "Um atendimento encontra-se ativo, finalize antes de iniciar outro atendimento.";
                return RedirectToPage("/Atendimento/Novo/Index");
            }

            if (id != null)
            {
                Input.Pessoa = await GetPessoa((Guid)id);           
                
                foreach(var e in await _appServiceEmpresa
                    .ConsultaRazaoSocialAsync(Input.Pessoa.CPF.MaskRemove())
                    .Where(s => s.Situacao_Cadastral != "BAIXADA"))
                    Input.Empresa = e;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostIncluirEmpresaAsync()
        {
            Input.Pessoa = await GetPessoa(Input.Pessoa.Id);
            Input.Empresa = await GetEmpresa(GetCNPJ);
            return Page();
        }

        public async Task<IActionResult> OnPostRemoverEmpresaAsync()
        {
            Input.Empresa = null;
            Input.Pessoa = await GetPessoa(Input.Pessoa.Id);
            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (Input.Pessoa == null)
            {
                return Page();
            }

            var atendimento = new Domain.Entity.Atendimento()
            {
                Protocolo = await GetProtoloco(),
                Data = DateTime.Now,
                Status = "Ativo",
                Ativo = true,
                Owner_AppUser_Id = User.Identity.Name
            };

            try
            {
                atendimento.Pessoa = _appServicePessoa.GetIdAsync(Input.Pessoa.Id);
                if (Input.Empresa != null)
                    atendimento.Empresa = _appServiceEmpresa.GetIdAsync(Input.Empresa.Id);
                await _appServiceAtendimento.AddAsync(atendimento);

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }

            return RedirectToPage("/Atendimento/Novo/Index");
        }       

    }
}
