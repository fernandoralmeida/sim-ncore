using Sim.Identity.Entity;

namespace Sim.UI.Web.Areas.Admin.ViewModel
{
    
    public class VMUserRoles : ApplicationUser
    {

        public IEnumerable<string> ListRoles { get; set; } 

    }
}
