using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Areas.Settings.Pages.Common
{
    public static class SettingsNavPages
    {
        public static string Secretarias => "Index";
        public static string Setores => "Setores";
        public static string Eventos => "Eventos";
        public static string Servicos => "Servicos";
        public static string Canal => "Canal";
        public static string SecretariaNavClass(ViewContext viewContext) => PageNavClass(viewContext, Secretarias);
        public static string SetoresNavClass(ViewContext viewContext) => PageNavClass(viewContext, Setores);
        public static string EventosNavClass(ViewContext viewContext) => PageNavClass(viewContext, Eventos);
        public static string ServicosNavClass(ViewContext viewContext) => PageNavClass(viewContext, Servicos);
        public static string CanalNavClass(ViewContext viewContext) => PageNavClass(viewContext, Canal);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
