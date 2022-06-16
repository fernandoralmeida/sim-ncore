using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace Sim.UI.Web.Pages.Planner
{
    
    using Sim.Application.Shared.Interface;
    using Sim.Domain.Shared.Entity;


    [Authorize]
    public class IndexModel : PageModel
    {

        private readonly IAppServicePlaner _planner;
        private readonly IMapper _mapper;

        public IndexModel(IAppServicePlaner planner,
            IMapper mapper) {

            _planner = planner;
            _mapper = mapper;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelPlanner Input { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime? InicioSemana { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime? FimSemana { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime? MeuDia { get; set; }

        public async Task OnGetAsync()
        {
            MeuDia = DateTime.Now.Date;
            PlannerTimer(DateTime.Now);
            
            var plnn = await _planner.GetMyPlanner(Input.DataInicial, Input.DataFinal, User.Identity.Name);

            if(!plnn.Any())
            {
                Input.Owner_AppUser_Id = User.Identity.Name;
                Input.Ativo = true;
                var t = Task.Run(() => _planner.Add(_mapper.Map<Planner>(Input)));
                await t;
            }

            foreach(Planner p in plnn)
            {
                Input = _mapper.Map<InputModelPlanner>(p);
            }   
        }

        public async Task OnPostAsync()
        {
            PlannerTimer(DateTime.Now.Date);
            
            var t = Task.Run(() => _planner.Update(_mapper.Map<Planner>(Input)));
            await t;
            
            var plnn = await _planner.GetMyPlanner(Input.DataInicial, Input.DataFinal, User.Identity.Name);

            foreach (Planner p in plnn)
            {
                Input = _mapper.Map<InputModelPlanner>(p);
            }
        }

        public async Task<IActionResult> OnPostToDateAsync() {

            PlannerTimer((DateTime)MeuDia);

            var plnn = await _planner.GetMyPlanner(Input.DataInicial, Input.DataFinal, User.Identity.Name);

            if (!plnn.Any())
            {
                Input = new();
                Input.Owner_AppUser_Id = User.Identity.Name;
                Input.Ativo = true;
                var t = Task.Run(() => _planner.Add(_mapper.Map<Planner>(Input)));
                await t;
            }

            foreach (Planner p in plnn)
            {
                Input = _mapper.Map<InputModelPlanner>(p);
            }

            return Page();
        }

        private void PlannerTimer(DateTime dia)
        {
            var dia_da_semana = dia.DayOfWeek;
            InicioSemana = dia.Date;
            FimSemana = dia.Date;

            switch (dia_da_semana)
            {
                case DayOfWeek.Sunday:
                    InicioSemana = dia.AddDays(0);
                    FimSemana = dia.AddDays(6);
                    break;

                case DayOfWeek.Monday:
                    InicioSemana = dia.AddDays(-1);
                    FimSemana = dia.AddDays(5);
                    break;

                case DayOfWeek.Tuesday:
                    InicioSemana = dia.AddDays(-2);
                    FimSemana = dia.AddDays(4);
                    break;

                case DayOfWeek.Wednesday:
                    InicioSemana = dia.AddDays(-3);
                    FimSemana = dia.AddDays(3);
                    break;

                case DayOfWeek.Thursday:
                    InicioSemana = dia.AddDays(-4);
                    FimSemana = dia.AddDays(2);
                    break;

                case DayOfWeek.Friday:
                    InicioSemana = dia.AddDays(-5);
                    FimSemana = dia.AddDays(1);
                    break;

                case DayOfWeek.Saturday:
                    InicioSemana = dia.AddDays(-6);
                    FimSemana = dia.AddDays(0);
                    break;

                default:
                    break;
            }

            Input.DataInicial = InicioSemana.Value.Date;
            Input.DataFinal = FimSemana.Value.Date;
        }
    }
}
