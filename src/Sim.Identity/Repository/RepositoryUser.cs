using Microsoft.EntityFrameworkCore;

namespace Sim.Identity.Repository
{
    using Entity;
    using Interfaces;
    using Context;

    public class RepositoryUser : IServiceUser
    {
        protected IdentityContext _db;
        public RepositoryUser(IdentityContext identity)
        {
            _db = identity;
        }
        public async Task<ApplicationUser> GetIdAsync(string id)
        {
            return await _db.AppUsers.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<ApplicationUser>> ListAllAsync()
        {
            return await _db.AppUsers.ToListAsync();
        }

        public async Task<bool> lockUnlockAsync(string id, bool lockUnlock)
        {
            var t = await _db.AppUsers.FindAsync(id);
            t.LockoutEnabled = lockUnlock;
            await _db.SaveChangesAsync();
            return lockUnlock;
        }
    }
}
