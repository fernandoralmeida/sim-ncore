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

using Sim.Domain.Cnpj.Entity;
using Sim.Application.Cnpj.Views;

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
            CreateMap<VMServicos, EServico>().ReverseMap();
            CreateMap<VMTipo, ETipo>().ReverseMap();
            CreateMap<VMCanal, ECanal>().ReverseMap();
            CreateMap<VMParceiros, EParceiro>().ReverseMap();
            CreateMap<VMBaseReceitaFederal, BaseReceitaFederal>().ReverseMap();
        }
    }
}
