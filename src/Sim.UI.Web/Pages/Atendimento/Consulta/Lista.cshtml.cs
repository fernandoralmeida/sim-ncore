using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using Sim.Domain.Entity;
using Sim.Application.Interfaces;
using Sim.UI.Web.Functions;

namespace Sim.UI.Web.Pages.Atendimento.Consultas
{


    [Authorize(Roles = "Admin_Global")]
    public class ListaModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;

        public ListaModel(IAppServiceAtendimento appServiceAtendimento)
        {
            _appServiceAtendimento = appServiceAtendimento;
            GetParam = new();
        }

        [TempData]
        public string StatusMessage { get; set; }

        public ParamModel GetParam { get; set; } 

        public IEnumerable<EAtendimento> ListaAtendimento { get; set; }

        public async Task OnGet(string p1, string p2, string p3, string p4,string p5, string p6, string p7, string p8, string p9)
        {

            try
            {

                GetParam.param1 = p1.Mask("##/##/####");
                GetParam.param2 = p2.Mask("##/##/####");
                GetParam.param3 = p3 != "0" ? p3.Mask("###.###.###-##") : "#";
                GetParam.param4 = p4 != "0" ? p6 : "#";
                GetParam.param5 = p5 != "0" ? p5.Mask("##.###.###/####-##") : "#";
                GetParam.param6 = p6 != "0" ? p6 : "#";
                GetParam.param7 = p7 != "0" ? p7.Mask("##.##-#-##") : "#";
                GetParam.param8 = p8 != "0" ? p8 : "#";
                GetParam.param9 = p9 != "0" ? p9 : "#";

                var d1 = Convert.ToDateTime(GetParam.param1);
                var d2 = Convert.ToDateTime(GetParam.param2);

                var param = new List<object>() {
                    d1,
                    d2,
                    GetParam.param3,
                    GetParam.param4,
                    GetParam.param5,
                    GetParam.param6,
                    GetParam.param7,
                    GetParam.param8,
                    GetParam.param9
                };

                ListaAtendimento = await _appServiceAtendimento.ListParamAsync(param);
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                ListaAtendimento = new List<EAtendimento>();
            }


        }


    }

}
