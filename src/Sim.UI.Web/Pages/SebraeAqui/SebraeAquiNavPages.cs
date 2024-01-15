using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.SebraeAqui
{
    public static class SebraeAquiNavPages
    {
        public static string Inicio => "Index";
        public static string RaeLancados => "Lançados";
        public static string Exportar => "Exportar";
        public static string InicioNavClass(ViewContext viewContext) => PageNavClass(viewContext, Inicio);
        public static string LancadosNavClass(ViewContext viewContext) => PageNavClass(viewContext, RaeLancados);
        public static string ExportNavClass(ViewContext viewContext) => PageNavClass(viewContext, Exportar);
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePageNSA"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
