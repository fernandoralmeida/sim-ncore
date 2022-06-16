using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.Shared
{
    public class LayoutNavPages
    {
        public static string Inicio => "Inicio";
        public static string InicioPAT => "InicioPAT";
        public static string InicioBPP => "InicioBPP";
        public static string InicioSA => "InicioSA";
        public static string InicioSE => "InicioSE";
        public static string InicioTriagem => "InicioTriagem";
        public static string Atendimento => "Atendimento";
        public static string AtendimentoDiario => "AtendimentoDiario";
        public static string AtendimentoAtivo => "AtendimentoAtivo";
        public static string AtendimentoConsulta => "AtendimentoConsulta";
        public static string AtendimentoStatus => "AtendimentoStatus";
        public static string Clientes => "Clientes";
        public static string ClientesEmpresas => "ClientesEmpresas";
        public static string ClientesPessoas => "ClientesPessoas";
        public static string Agenda => "Agenda";
        public static string AgendaEventos => "AgendaEventos";
        public static string AgendaPlanner => "AgendaPlanner";
        public static string PowerBI => "PowerBI";
        public static string PowerBIAtendimento => "PowerBIAtendimento";
        public static string PowerBIEmpresas => "PowerBIEmpresas";
        public static string PowerBIPessoas => "PowerBIPessoas";
        public static string Configuracoes => "Configuracoes";
        public static string ConfigSistema => "ConfigSistema";
        public static string ConfigContas => "ConfigContas";
        public static string Login => "Login";
        public static string LoginPerfil => "LoginPerfil";
        public static string LoginOut => "LoginOut";
        public static string InicioNavClass(ViewContext viewContext) => PageNavClass(viewContext, Inicio);
        public static string InicioPATNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, InicioPAT);
        public static string InicioBPPNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, InicioBPP);
        public static string InicioSANavClass(ViewContext viewContext) => PageNavClassLi(viewContext, InicioSA);
        public static string InicioSENavClass(ViewContext viewContext) => PageNavClassLi(viewContext, InicioSE);
        public static string InicioTriagemNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, InicioTriagem);
        public static string AtendimentoNavClass(ViewContext viewContext) => PageNavClass(viewContext, Atendimento);
        public static string AtendimentoDiarioNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, AtendimentoDiario);
        public static string AtendimentoAtivoNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, AtendimentoAtivo);
        public static string AtendimentoConsultaNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, AtendimentoConsulta);
        public static string AtendimentoStatusNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, AtendimentoStatus);
        public static string ClientesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Clientes);
        public static string ClientesEmpresasNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, ClientesEmpresas);
        public static string ClientesPessoasNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, ClientesPessoas);
        public static string AgendaNavClass(ViewContext viewContext) => PageNavClass(viewContext, Agenda);
        public static string AgendaEventosNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, AgendaEventos);
        public static string AgendaPlannerNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, AgendaPlanner);
        public static string PowerBINavClass(ViewContext viewContext) => PageNavClass(viewContext, PowerBI);
        public static string PowerBIAtendimentoNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, PowerBIAtendimento);
        public static string PowerBIEmpresasNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, PowerBIEmpresas);
        public static string PowerBIPessoasNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, PowerBIPessoas);
        public static string ConfiguracoesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Configuracoes);
        public static string ConfigSistemaNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, ConfigSistema);
        public static string ConfigContasNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, ConfigContas);
        public static string LoginNavClass(ViewContext viewContext) => PageNavClass(viewContext, Login);
        public static string LoginPerfilNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, LoginPerfil);
        public static string LoginOutNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, LoginOut);
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        private static string PageNavClassLi(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePageLi"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

    }
}
