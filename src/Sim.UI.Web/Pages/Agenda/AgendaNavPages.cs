using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.Agenda
{
    public static class AgendaNavPages
    {
        public static string EventoAtivo => "EAtivo";
        public static string EventoFinalizado => "EFinalizado";
        public static string EventoCancelado => "ECancelado";
        public static string EventoAtivoClass(ViewContext viewContext) => PageNavClass(viewContext, EventoAtivo);
        public static string EventoFinalizadoClass(ViewContext viewContext) => PageNavClass(viewContext, EventoFinalizado);
        public static string EventoCanceladoClass(ViewContext viewContext) => PageNavClass(viewContext, EventoCancelado);
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePageEvento"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
