using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Cnpj.Interfaces;
using Sim.Domain.Cnpj.Entity;

namespace Sim.UI.Web.Areas.SimBI.Pages.Empresas
{
    public static class EmpNavPages
    {
        public static string Empresa => "Empresas";
        public static string Cnae => "Cnae";
        public static string EmpNavClass(ViewContext viewContext) => PageNavClass(viewContext, Empresa);
        public static string CnaeNavClass(ViewContext viewContext) => PageNavClass(viewContext, Cnae);
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["BIActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
