
using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryEmpregos : RepositoryBase<Empregos>, IRepositoryEmpregos
    {
        public RepositoryEmpregos(ApplicationContext applicationContext) : base(applicationContext) { }

        public async Task<IEnumerable<Empregos>> DoListEmpregosAsync()
        {
            return await _db.Emprego.Include(e => e.Empresa)
                                    .Include(p => p.Pessoa).ToListAsync();
        }

        public async Task<IEnumerable<Empregos>> DoListEmpregosAsyncBy(string param)
        {
            return await _db.Emprego.Include(e => e.Empresa)
                                    .Include(p => p.Pessoa)
                .Where(e => e.Empresa.CNPJ.Contains(param) ||
                            e.Empresa.Nome_Empresarial.Contains(param) ||
                            e.Empresa.Atividade_Principal.Contains(param) ||
                            e.Pessoa.CPF.Contains(param) ||
                            e.Pessoa.Nome.Contains(param) ||
                            e.Ocupacao.Contains(param) ||
                            e.Inclusivo.Contains(param) ||
                            e.Genero.Contains(param))
                .AsNoTracking()
                .OrderBy(o => o.Data)
                .ToListAsync();
        }

        public async Task<IEnumerable<Empregos>> DoListEmpregosAsyncByAno(int ano)
        {
            return await _db.Emprego.Include(e => e.Empresa)
                                    .Include(p => p.Pessoa)
                .Where(e => e.Data.Value.Year == ano)
                .AsNoTracking()
                .OrderBy(o => o.Data)
                .ToListAsync();
        }

        public async Task<Empregos> GetEmpregoByIdAsync(Guid id)
        {
            return await _db.Emprego.Include(e => e.Empresa)
                                    .Include(p => p.Pessoa)
                            .Where(e => e.Id == id)
                            .SingleOrDefaultAsync();
        }
    }
}
