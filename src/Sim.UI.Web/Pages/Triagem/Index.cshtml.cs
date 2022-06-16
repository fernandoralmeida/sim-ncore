using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Sim.UI.Web.Pages.Triagem
{
    
    using Sim.Cross.Identity;
    using Sim.Application.Shared.Interface;

    //[Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppServiceAtendimento _appAtendimento;
        private readonly IAppServiceStatusAtendimento _appServiceStatusAtendimento;

        [BindProperty(SupportsGet = true)]
        public InputModelIndex Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public IEnumerable<InputModelIndex> ListaPAT { get; set; }

        [BindProperty(SupportsGet = true)]
        public IEnumerable<InputModelIndex> ListaBPP { get; set; }

        [BindProperty(SupportsGet = true)]
        public IEnumerable<InputModelIndex> ListaSA { get; set; }

        [BindProperty(SupportsGet = true)]
        public IEnumerable<InputModelIndex> ListaSE { get; set; }

        public class InputModelIndex
        {
            public string Atendente { get; set; }
            public string Status { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IndexModel(UserManager<ApplicationUser> userManager,
            IAppServiceAtendimento appAtendimento,
            IAppServiceStatusAtendimento appServiceStatusAtendimento)
        {
            _userManager = userManager;
            _appAtendimento = appAtendimento;
            _appServiceStatusAtendimento = appServiceStatusAtendimento;
        }

        private async Task LoadPATAsync()
        {
            var list = new List<InputModelIndex>();
            var pat = await _userManager.GetUsersInRoleAsync("M_Pat");

            foreach (ApplicationUser s in pat)
            {

                var t = await _appServiceStatusAtendimento.ListByUser(s.UserName);

                if (t.Any())

                    if (t.FirstOrDefault().Online)
                    {
                        var ativo = _appAtendimento.AtendimentoAtivo(s.UserName);

                        if (ativo.Any())
                            list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Em Atendimento" });

                        else
                            list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Disponível" });
                    }
                    else
                        list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Indisponível" });
                else
                {
                    var ativo = _appAtendimento.AtendimentoAtivo(s.UserName);

                    if (ativo.Any())
                        list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Em Atendimento" });

                    else
                        list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Disponível" });
                }

            }

            ListaPAT = list;
        }

        private async Task LoadBPPAsync()
        {
            var list = new List<InputModelIndex>();
            var pat = await _userManager.GetUsersInRoleAsync("M_BancoPovo");

            foreach (ApplicationUser s in pat)
            {
                var t = await _appServiceStatusAtendimento.ListByUser(s.UserName);

                if (t.Any())

                    if (t.FirstOrDefault().Online)
                    {
                        var ativo = _appAtendimento.AtendimentoAtivo(s.UserName);

                        if (ativo.Any())
                            list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Em Atendimento" });

                        else
                            list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Disponível" });
                    }
                    else
                        list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Indisponível" });
                else
                {
                    var ativo = _appAtendimento.AtendimentoAtivo(s.UserName);

                    if (ativo.Any())
                        list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Em Atendimento" });

                    else
                        list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Disponível" });
                }
            }

            ListaBPP = list;
        }

        private async Task LoadSAAsync()
        {
            var list = new List<InputModelIndex>();
            var pat = await _userManager.GetUsersInRoleAsync("M_Sebrae");

            foreach (ApplicationUser s in pat)
            {
                var t = await _appServiceStatusAtendimento.ListByUser(s.UserName);

                if (t.Any())

                    if (t.FirstOrDefault().Online)
                    {
                        var ativo = _appAtendimento.AtendimentoAtivo(s.UserName);

                        if (ativo.Any())
                            list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Em Atendimento" });

                        else
                            list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Disponível" });
                    }
                    else
                        list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Indisponível" });
                else
                {
                    var ativo = _appAtendimento.AtendimentoAtivo(s.UserName);

                    if (ativo.Any())
                        list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Em Atendimento" });

                    else
                        list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Disponível" });
                }
            }

            ListaSA = list;
        }

        private async Task LoadSEAsync()
        {
            var list = new List<InputModelIndex>();
            var pat = await _userManager.GetUsersInRoleAsync("M_SalaEmpreendedor");

            foreach (ApplicationUser s in pat)
            {
                var t = await _appServiceStatusAtendimento.ListByUser(s.UserName);

                if (t.Any())

                    if (t.FirstOrDefault().Online)
                    {
                        var ativo = _appAtendimento.AtendimentoAtivo(s.UserName);

                        if (ativo.Any())
                            list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Em Atendimento" });

                        else
                            list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Disponível" });
                    }
                    else
                        list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Indisponível" });
                else
                {
                    var ativo = _appAtendimento.AtendimentoAtivo(s.UserName);

                    if (ativo.Any())
                        list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Em Atendimento" });

                    else
                        list.Add(new InputModelIndex() { Atendente = s.Name + " " + s.LastName, Status = "Disponível" });
                }
            }

            ListaSE = list;

        }
        public async Task<IActionResult> OnGetAsync()
        {
            await LoadPATAsync();
            await LoadBPPAsync();
            await LoadSAAsync();
            await LoadSEAsync();
            return Page();
        }
    }
}
