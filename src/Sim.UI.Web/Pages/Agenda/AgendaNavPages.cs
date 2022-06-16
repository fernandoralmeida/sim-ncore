using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.Agenda
{
    public static class AgendaNavPages
    {
        public static string Inicio => "Index";
        public static string NovoEvento => "Novo";
        public static string NovaInscricao => "Inscricao.Evento";
        public static string EventoEdit => "Edit";
        public static string EventoList => "Evento.List";
        public static string InicioNavClass(ViewContext viewContext) => PageNavClass(viewContext, Inicio);
        public static string NovoEventoNavClass(ViewContext viewContext) => PageNavClass(viewContext, NovoEvento);
        public static string NovaInscricaoNavClass(ViewContext viewContext) => PageNavClass(viewContext, NovaInscricao);
        public static string EventoEditClass(ViewContext viewContext) => PageNavClass(viewContext, EventoEdit);
        public static string EventoListClass(ViewContext viewContext) => PageNavClass(viewContext, EventoList);
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
