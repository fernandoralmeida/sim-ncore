using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.Pat
{
    public static class PatNav
    {
        public static string Inicio => "Index";
        public static string Novo => "Novo";
        public static string Consulta => "Consulta";
        public static string Finalizada => "Finalizada";
        public static string InicioNavClass(ViewContext viewContext) => PageNavClass(viewContext, Inicio);
        public static string NovoNavClass(ViewContext viewContext) => PageNavClass(viewContext, Novo);
        public static string ConsultaNavClass(ViewContext viewContext) => PageNavClass(viewContext, Consulta);
        public static string FinalizadaNavClass(ViewContext viewContext) => PageNavClass(viewContext, Finalizada);
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePagePat"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}