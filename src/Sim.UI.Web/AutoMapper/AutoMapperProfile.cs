using AutoMapper;
using Sim.Domain.Entity;
using Sim.Application.WebService.RWS.Entity;
using Sim.UI.Web.Pages.Cliente;
using Sim.UI.Web.Pages.Empresa;
using Sim.UI.Web.Pages.Atendimento;
using Sim.UI.Web.Pages.Planner;
using Sim.UI.Web.Pages.Agenda;

namespace Sim.UI.Web.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<InputModelPessoa, Pessoa>();
            CreateMap<Pessoa, InputModelPessoa>().ReverseMap();

            CreateMap<VMEmpresa, CNPJ>();
            CreateMap<CNPJ, VMEmpresa>().ReverseMap();

            CreateMap<VMEmpresa, Empresas>();
            CreateMap<Empresas, VMEmpresa>().ReverseMap();

            CreateMap<InputModelAtendimento, Atendimento>();
            CreateMap<Atendimento, InputModelAtendimento>().ReverseMap();

            CreateMap<InputModelEvento, Evento>();
            CreateMap<Evento, InputModelEvento>().ReverseMap();

            CreateMap<InputModelInscricao, Inscricao>();
            CreateMap<Inscricao, InputModelInscricao>().ReverseMap();

            CreateMap<InputModelPlanner, Planner>();
            CreateMap<Planner, InputModelPlanner>().ReverseMap();
        }
    }
}
