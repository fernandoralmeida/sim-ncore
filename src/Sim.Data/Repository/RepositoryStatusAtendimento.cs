using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryStatusAtendimento : RepositoryBase<StatusAtendimento>, IRepositoryStatusAtendimento
    {
        public RepositoryStatusAtendimento(ApplicationContext dbContext)
            : base(dbContext)
        { }

        public async Task<StatusAtendimento> GetIdAsync(Guid id) {
            return await _db.StatusAtendimento.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<StatusAtendimento>> ListAllAsync() {
            return await _db.StatusAtendimento.AsNoTrackingWithIdentityResolution().ToListAsync();
        }

        public async Task<IEnumerable<StatusAtendimento>> ListUserAsync(string username) {
            return await _db.StatusAtendimento.Where(s => s.UnserName == username).AsNoTrackingWithIdentityResolution().ToListAsync();
        }

        public async Task<StatusAtendimento> MyStatusAsync(string username) {   
            return await _db.StatusAtendimento.Where(s => s.UnserName == username).FirstOrDefaultAsync();
        }
    }
}
