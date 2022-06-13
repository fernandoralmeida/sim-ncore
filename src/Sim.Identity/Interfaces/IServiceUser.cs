
namespace Sim.Identity.Interfaces
{
    using Entity;
    public interface IServiceUser
    {
        Task<ApplicationUser> GetIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> ListAllAsync();
        Task<bool> lockUnlockAsync(string id, bool lockUnlock);
    }
}
