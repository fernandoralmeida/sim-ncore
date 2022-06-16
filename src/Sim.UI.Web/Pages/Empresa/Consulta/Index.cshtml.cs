using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.Empresa.Consulta
{
    using Sim.Domain.Cnpj.Entity;
    using Sim.Application.SDE.Interface;
    using Functions;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEmpresa _appServiceEmpresa;
        
        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }
       
        public class InputModel
        {
            [DisplayName("Data Inicial")]
            [DataType(DataType.Date)]
            public DateTime? DataI { get; set; }

            [DisplayName("Data Final")]
            [DataType(DataType.Date)]
            public DateTime? DataF { get; set; }

            public string CNAE { get; set; }            
            
            public string Logradouro { get; set; }

            public string Bairro { get; set; }

            public ICollection<BaseReceitaFederal> ListaEmpresas { get; set; }
        }

        public IndexModel(IAppServiceEmpresa appServiceEmpresa)
        {
            _appServiceEmpresa = appServiceEmpresa;
        }

        public void OnGet()
        {

        }
    }
}
