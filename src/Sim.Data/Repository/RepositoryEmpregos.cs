
using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryEmpregos : RepositoryBase<Empregos>, IRepositoryEmpregos
    {
        public RepositoryEmpregos(ApplicationContext applicationContext) : base(applicationContext) { }

        public async Task<IEnumerable<Empregos>> GetAllEmpregosAsync()
        {
            var t = _db.Emprego.Include(e => e.Empresa).ToListAsync();
            await t;
            return t.Result;
        }

        public async Task<IEnumerable<Empregos>> GetAllEmpregosAsync(string cnpj)
        {
            var t = Task.Run(() => _db.Emprego.Include(e => e.Empresa).Where(s => s.Empresa.CNPJ == cnpj));
            await t;
            return t.Result;
        }

        public async Task<IEnumerable<Empregos>> GetByIdAsync(Guid id)
        {
            var t = Task.Run(() => _db.Emprego.Include(e => e.Empresa).Where(s => s.Id == id));
            await t;
            return t.Result;
        }

        public Task<IEnumerable<Empregos>> ListEmpregosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Empregos>> ListEmpregosAsync(string cnpj)
        {
            throw new NotImplementedException();
        }
    }
}
