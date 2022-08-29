using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Areas.SimBI.Pages.Atendimentos
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceBIAtendimento _biantendimento;
        private readonly IAppServiceAtendimento _appAtendimento;
        private readonly IAppServiceSetor _appSetores;
        private readonly IAppServiceSecretaria _appServiceSecretaria;

        [BindProperty(SupportsGet = true)]
        public Secretaria Sec { get; set; }

        [BindProperty(SupportsGet = true)]
        public IEnumerable<BIAtendimentos> Setores { get; set; }

        [BindProperty(SupportsGet = true)]
        public BIAtendimentos AppUsers { get; set; }

        [BindProperty(SupportsGet = true)]
        public BIAtendimentos Atendimentos_List { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Ano { get; set; }

        public SelectList Secretarias { get; set; }

        public IndexModel(IAppServiceAtendimento appAtendimento,
            IAppServiceSecretaria appServiceSecretaria,
            IAppServiceSetor appsetores)
        {
            _appAtendimento = appAtendimento;
            _appServiceSecretaria = appServiceSecretaria;
            _appSetores = appsetores;
        }

        private async Task LoadSecretaria()
        {
            var s = await _appServiceSecretaria.ListAllAsync();
            if (s != null)
                Secretarias = new SelectList(s, nameof(Sec.Nome), nameof(Sec.Nome), null);

            Sec.Nome = Secretarias.SingleOrDefault().Text;
        }

        private async Task LoadAsync()
        { 
            int n;
            bool isNumeric = int.TryParse(Ano, out n);
            if(isNumeric)
            {
                int _ano = Convert.ToInt32(Ano);
                if(_ano > 0)
                {
                    var nperiodo = new DateTime(_ano, 1, 1);
                    Atendimentos_List  = await _appAtendimento.ToListBIAtendimentos(nperiodo);
                    var _list = new List<BIAtendimentos>();
                    var setores = await _appSetores.ListSetorOwnerAsync(Sec.Nome);
                    foreach(var s in setores)
                    {
                        _list.Add(await _appAtendimento.ToListBIAtendimentosSetor(nperiodo, s.Nome));
                    }
                    Setores = _list;
                    AppUsers = await _appAtendimento.ToListBIAtendimentosAppUser(nperiodo);
                }
            }            
        }

        public async Task OnGetAsync()
        {
            Ano = DateTime.Now.Year.ToString();
            await LoadSecretaria();
            await LoadAsync();
        }

        public JsonResult OnGetPreview(string id, string id2, string mth, int y)
        {
            return null;
        }

        public JsonResult OnGetServicoPreview(string id, string mth, int y)
        {
            return null;
        }

        public JsonResult OnGetCanalPreview(string id, string id2,  string mth, int y)
        {
            return null;
        }

        public async Task OnPostAsync()
        {
            await LoadSecretaria();
            await LoadAsync();
        }

    }
}
