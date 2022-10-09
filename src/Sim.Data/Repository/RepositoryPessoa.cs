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
            return await _db.Pessoa.Where(c => c.CPF == cpf).OrderBy(c => c.Nome)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<IEnumerable<Pessoa>> ConsultaNomeAsync(string nome)
        {
            return await _db.Pessoa.Where(c => c.Nome.Contains(nome) || c.Nome_Social.Contains(nome)).OrderBy(c => c.Nome)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<IEnumerable<Pessoa>> DoListAsyncBy(string param)
        {
            return await _db.Pessoa.Where(c => c.CPF.Contains(param) ||
                                        c.Nome.Contains(param) ||
                                        c.Nome_Social.Contains(param))
                                        .OrderBy(c => c.Nome)
                                        .AsNoTrackingWithIdentityResolution()
                                        .ToListAsync();
        }

        public async Task<Pessoa> GetIdAsync(Guid id)
        {
            return await _db.Pessoa.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Pessoa>> ListAllAsync()
        {
            return await _db.Pessoa
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<IEnumerable<Pessoa>> ListTop10Async()
        {
            return await (from p in _db.Pessoa orderby p.Ultima_Alteracao descending select p).Take(10)
                                                                                            .AsNoTrackingWithIdentityResolution()
                                                                                            .ToListAsync();            
        }

    }
}
