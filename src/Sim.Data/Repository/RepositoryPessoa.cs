using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryPessoa : RepositoryBase<Pessoa>, IRepositoryPessoa
    {
        public RepositoryPessoa(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public async Task<IEnumerable<Pessoa>> ConsultaCPFAsync(string cpf)
        {
            return await _db.Pessoa.Where(c => c.CPF == cpf).OrderBy(c => c.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Pessoa>> ConsultaNomeAsync(string nome)
        {
            return await _db.Pessoa.Where(c => c.Nome.Contains(nome) || c.Nome_Social.Contains(nome)).OrderBy(c => c.Nome).ToListAsync();
        }

        public async Task<Pessoa> GetIdAsync(Guid id)
        {
            return await _db.Pessoa.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Pessoa>> ListAllAsync()
        {
            return await _db.Pessoa.ToListAsync();
        }

        public async Task<IEnumerable<Pessoa>> ListTop10Async()
        {
            return await _db.Pessoa.Take(10).OrderByDescending(d => d.Ultima_Alteracao).ToListAsync();
        }

    }
}
