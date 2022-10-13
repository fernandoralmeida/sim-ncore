using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.Pat
{
    [Authorize(Roles = "Administrador,M_Pat")]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpregos _appEmpregos;
        private readonly IAppServicePessoa _appPessoa;
        private readonly IAppServiceEmpresa _appEmpresa;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public string InputSearch { get; set; }

        [BindProperty]
        public string CNPJ { get; set; }
        public IEnumerable<Empregos> ListaEmpregos { get; set; }
        public IEnumerable<Empresas> ListaEmpresas { get; set; }

        public IndexModel(IAppServiceEmpregos appServiceEmpregos,
            IAppServiceEmpresa appServiceEmpresa,
            IAppServicePessoa appServicePessoa)
        {
            _appEmpregos = appServiceEmpregos;
            _appPessoa = appServicePessoa;
            _appEmpresa = appServiceEmpresa;
        }
        public async Task OnGetAsync()
        {
            ListaEmpregos = await _appEmpregos.DoListAsync(
                s => s.Status == "Ativo" ||
                s.Status == Empregos.EStatus.Ativa.ToString());
        }

        public async Task OnGetVagaPreenchidaAsync(Guid id)
        {
            try {
                var _vaga = await _appEmpregos.SingleIdAsync(id);
                _vaga.Status = Empregos.EStatus.Finalizada.ToString();
                await _appEmpregos.UpdateAsync(_vaga);
                ListaEmpregos = await _appEmpregos.DoListAsync(
                    s => s.Status == "Ativo" ||
                    s.Status == Empregos.EStatus.Ativa.ToString());
            }
            catch (Exception ex) {
                StatusMessage = "Erro: " + ex.Message;
            }
        }
        public async Task OnPostAsync(){
            ListaEmpregos = await _appEmpregos.DoListAsync(e => e.Empresa.CNPJ.Contains(InputSearch) ||
                            e.Empresa.Nome_Empresarial.Contains(InputSearch) ||
                            e.Empresa.Atividade_Principal.Contains(InputSearch) ||
                            e.Pessoa.CPF.Contains(InputSearch) ||
                            e.Pessoa.Nome.Contains(InputSearch) ||
                            e.Ocupacao.Contains(InputSearch) ||
                            e.Inclusivo.Contains(InputSearch) ||
                            e.Status.Contains(InputSearch) ||
                            e.Genero.Contains(InputSearch));
        }

        public async Task<JsonResult> OnGetClienteAsync(string id){                        
            return await Task.Run(async () => {                    
                var _emp = await _appEmpresa.SingleIdAsync(new Guid(id));
                if(_emp != null)
                    return new JsonResult(_emp);
                else
                    return new JsonResult(await _appPessoa.SingleIdAsync(new Guid(id)));       
            });                
        }
    }
}
