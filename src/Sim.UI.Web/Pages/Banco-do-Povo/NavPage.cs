using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.BancoPovo;

public static class NavPage {
    public static string Inicio => "Index";
    public static string Novo => "Novo";
    public static string Edit => "Edit";
    public static string Consulta => "Consulta";
    public static string InicioBPClass(ViewContext viewContext) => PageNavClass(viewContext, Inicio);
    public static string NovoBPClass(ViewContext viewContext) => PageNavClass(viewContext, Novo);
    public static string EditBPClass(ViewContext viewContext) => PageNavClass(viewContext, Edit);
    public static string ConsultaBPClass(ViewContext viewContext) => PageNavClass(viewContext, Consulta);
    private static string PageNavClass(ViewContext viewContext, string page)
    {
        var activePage = viewContext.ViewData["ActivePageBPP"] as string
            ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
        return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
    }
}