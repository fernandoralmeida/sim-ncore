namespace Sim.UI.Web.Areas.Admin.Pages;

public static class Admin
{
    public const string Global = "Admin Global";
    public const string Account = "Admin Account";
    public const string Settings = "Admin Settings";

    public static IEnumerable<string> ToList()
    {
        return new List<string>() { Global, Account, Settings };
    }

    
}