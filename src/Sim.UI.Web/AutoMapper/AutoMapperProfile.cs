using AutoMapper;
using Sim.Domain.Entity;

using Sim.Domain.Organizacao.Model;
using Sim.Domain.Evento.Model;

using Sim.Application.VM;
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
            CreateMap<Pessoa, InputModelPessoa>().ReverseMap();
            CreateMap<CNPJ, VMEmpresa>().ReverseMap();
            CreateMap<Empresas, VMEmpresa>().ReverseMap();
            CreateMap<Atendimento, InputModelAtendimento>().ReverseMap();
            CreateMap<EEvento, InputModelEvento>().ReverseMap();
            CreateMap<Inscricao, InputModelInscricao>().ReverseMap();
            CreateMap<Planner, InputModelPlanner>().ReverseMap();
            CreateMap<VMSecretaria, EOrganizacao>().ReverseMap();
        }
    }
}
