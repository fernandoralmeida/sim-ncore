using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Areas.SimBI.Pages.Atendimentos
{
    public static class NavPages
    {
        public static string Jan => "Jan";
        public static string Fev => "Fev";
        public static string Mar => "Mar";
        public static string Abr => "Abr";
        public static string Mai => "Mai";
        public static string Jun => "Jun";
        public static string Jul => "Jul";
        public static string Ago => "Ago";
        public static string Set => "Set";
        public static string Out => "Out";
        public static string Nov => "Nov";
        public static string Dez => "Dez";
        public static string Year => "Year";
        public static string JanNavClass(ViewContext viewContext) => PageNavClass(viewContext, Jan);
        public static string FevNavClass(ViewContext viewContext) => PageNavClass(viewContext, Fev);
        public static string MarNavClass(ViewContext viewContext) => PageNavClass(viewContext, Mar);
        public static string AbrNavClass(ViewContext viewContext) => PageNavClass(viewContext, Abr);
        public static string MaiNavClass(ViewContext viewContext) => PageNavClass(viewContext, Mai);
        public static string JunNavClass(ViewContext viewContext) => PageNavClass(viewContext, Jun);
        public static string JulNavClass(ViewContext viewContext) => PageNavClass(viewContext, Jul);
        public static string AgoNavClass(ViewContext viewContext) => PageNavClass(viewContext, Ago);
        public static string SetNavClass(ViewContext viewContext) => PageNavClass(viewContext, Set);
        public static string OutNavClass(ViewContext viewContext) => PageNavClass(viewContext, Out);
        public static string NovNavClass(ViewContext viewContext) => PageNavClass(viewContext, Nov);
        public static string DezNavClass(ViewContext viewContext) => PageNavClass(viewContext, Dez);
        public static string YearNavClass(ViewContext viewContext) => PageNavClass(viewContext, Year);
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActiveButton"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "btn" : "btn-flat";
        }
    }
}
