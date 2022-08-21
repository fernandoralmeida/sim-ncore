using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{

    public class RepositoryEmpresa : RepositoryBase<Empresas>, IRepositoryEmpresa
    {
        public RepositoryEmpresa(ApplicationContext dbContext)
            :base(dbContext)
        {  }

        public async Task<IEnumerable<Empresas>> ConsultaCNPJAsync(string cnpj)
        {
            return await _db.Empresa.Where(p=>p.CNPJ.Contains(cnpj)).ToListAsync();
        }

        public async Task<IEnumerable<Empresas>> ConsultaRazaoSocialAsync(string name)
        {
            return await _db.Empresa.Where(p => p.Nome_Empresarial.Contains(name)).ToListAsync(); ;
        }

        public async Task<IEnumerable<Empresas>> DoListAsyncBy(string param)
        {
            return await _db.Empresa.Where(s => s.CNPJ.Contains(param)||
                s.Nome_Empresarial.Contains(param) ||
                s.CNAE_Principal.Contains(param) ||
                s.Atividade_Principal.Contains(param) ||
                s.Atividade_Secundarias.Contains(param) ||
                s.Logradouro.Contains(param) ||
                s.Bairro.Contains(param))
                .OrderByDescending(o => o.Data_Abertura)
                .AsNoTracking()
                .ToListAsync();  
        }

        public async Task<Empresas> GetIdAsync(Guid id)
        {
            return await _db.Empresa
                .Include(a => a.Atendimentos)
                .Include(i => i.Inscricoes)
                .Include(e => e.VagasEmpregos)
                .Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Empresas>> ListAllAsync()
        {
            return await _db.Empresa.ToListAsync();
        }

        public async Task<IEnumerable<Empresas>> ListEmpresasAsync(List<object> lparam)
        {

            var cnpj = lparam[0] != null ? (string)lparam[0] : "";
            var razaosocial = lparam[1] != null ? (string)lparam[1] : "";
            var cnae = lparam[2] != null ? (string)lparam[2] : "";
            var logradouro = lparam[3] != null ? (string)lparam[3] : "";
            var bairro = lparam[4] != null ? (string)lparam[4] : "";

            if(cnpj == "" && razaosocial == "" && cnae == "" && logradouro == "" && bairro == "")
                return new List<Empresas>();


            return await _db.Empresa.Where(s => s.CNPJ.Contains(cnpj))
                .Where(s => s.Nome_Empresarial.Contains(razaosocial))
                .Where(s => s.CNAE_Principal.Contains(cnae))
                .Where(s => s.Logradouro.Contains(logradouro))
                .Where(s => s.Bairro.Contains(bairro))
                .OrderByDescending(o => o.Data_Abertura)
                .AsNoTracking()
                .ToListAsync();            
        }

        public async Task<IEnumerable<Empresas>> ListTop20Async()
        {
            return await _db.Empresa.Take(20).ToListAsync();
        }
    }
}
