using AutoMapper;
using Sim.Domain.Entity;
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
            CreateMap<Evento, InputModelEvento>().ReverseMap();
            CreateMap<Inscricao, InputModelInscricao>().ReverseMap();
            CreateMap<Planner, InputModelPlanner>().ReverseMap();
            CreateMap<VMPrefeitura, EPrefeitura>().ConstructUsing(c => new EPrefeitura(c.Id, c.Nome, c.Cidade, c.UF, c.Ativo)).ReverseMap();
            CreateMap< VMSecretaria, Secretaria>().ReverseMap();
        }
    }
}
