using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Application.Cnpj.Interfaces;
using Sim.Domain.Cnpj.Entity;
using System.Diagnostics;

namespace Sim.UI.Web.Areas.SimBI.Pages.Empresas
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceCnpj _appEmpresa;

        [BindProperty(SupportsGet = true)]
        public IEnumerable<BIEmpresas> ListEmpresas { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Municipio { get; set; }
            public int Ano { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IndexModel(IAppServiceCnpj appEmpresa)
        {
            _appEmpresa = appEmpresa;
            Input = new();
        }
        public async Task OnGetAsync(string m)
        {
            Stopwatch _timer = new();
            StatusMessage = "";

            if (string.IsNullOrEmpty(m))
                Input.Municipio = "6607";
            else
                Input.Municipio = m;

            Input.Ano = DateTime.Today.Year;
            _timer.Start();
            ListEmpresas = await _appEmpresa.DoListBIEmpresasAsync(Input.Municipio, Input.Ano);
            _timer.Stop();
            StatusMessage = $"{_timer.Elapsed:hh\\:mm\\:ss\\.fff}";
        }

        public async Task OnPostAsync(string m)
        {
            Stopwatch _timer = new();
            if (string.IsNullOrEmpty(m))
                Input.Municipio = "6607";
            else
                Input.Municipio = m;

            _timer.Start();
            ListEmpresas = await _appEmpresa.DoListBIEmpresasAsync(Input.Municipio, Input.Ano);
            StatusMessage = $"{_timer.Elapsed:hh\\:mm\\:ss\\.fff}";
        }
    }
}
