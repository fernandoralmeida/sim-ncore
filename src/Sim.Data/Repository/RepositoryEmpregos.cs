
using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryEmpregos : RepositoryBase<Empregos>, IRepositoryEmpregos
    {
        public RepositoryEmpregos(ApplicationContext applicationContext) : base(applicationContext) { }

        public async Task<Empregos> GetIdAsync(Guid id)
        {
            return await _db.Emprego.Include(e => e.Empresa).Where(o=>o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Empregos>> ListAllAsync()
        {
            return await _db.Emprego.Include(e => e.Empresa).ToListAsync();
        }

        public async Task<IEnumerable<Empregos>> ListEmpregosAsync()
        {
           return await _db.Emprego.Include(e => e.Empresa).ToListAsync();
        }

        public async Task<IEnumerable<Empregos>> ListEmpregosAsync(string cnpj)
        {
            return await _db.Emprego.Include(e => e.Empresa).Where(s => s.Empresa.CNPJ == cnpj).ToListAsync();
        }
    }
}
