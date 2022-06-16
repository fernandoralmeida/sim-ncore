using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.Empresa
{
    public static class EmpresaNavPages
    {
        public static string Inicio => "Index";
        public static string Consulta => "Consulta";
        public static string Relatórios => "Report";
        public static string MigraData => "Migra.data";
        public static string Jucesp => "Jucesp";
        public static string InicioNavClass(ViewContext viewContext) => PageNavClass(viewContext, Inicio);
        public static string ConsultaNomeNavClass(ViewContext viewContext) => PageNavClass(viewContext, Consulta);
        public static string ReportNavClass(ViewContext viewContext) => PageNavClass(viewContext, Relatórios);
        public static string MigraDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, MigraData);
        public static string JucespNavClass(ViewContext viewContext) => PageNavClass(viewContext, Jucesp);
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
