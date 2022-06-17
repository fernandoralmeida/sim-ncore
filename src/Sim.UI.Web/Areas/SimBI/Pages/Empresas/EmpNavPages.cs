using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Areas.SimBI.Pages.Empresas
{
    public static class EmpNavPages
    {
        public static string Empresa => "Empresas";
        public static string EmpNavClass(ViewContext viewContext) => PageNavClass(viewContext, Empresa);
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["BIEmpActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : "active";
        }
    }
}
