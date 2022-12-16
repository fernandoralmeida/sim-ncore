using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryAtendimento : RepositoryBase<EAtendimento>, IRepositoryAtendimento
    {
        public RepositoryAtendimento(ApplicationContext dbContext)
            :base(dbContext)
        {   }

        public async Task<IEnumerable<EAtendimento>> DoListAendimentosAsyncBy(string param)
        {
            return await _db.Atendimento
                        .Include(p => p.Pessoa)
                        .Include(e => e.Empresa)
                        .Include(s => s.Sebrae)
                        //.Where(a => a.Data.Value.Date >= dataI && a.Data.Value.Date <= dataF)
                        .Where(a => a.Status == "Finalizado" && a.Ativo == true)
                        .Where(a => a.Pessoa.CPF.Contains(param) ||
                        a.Pessoa.Nome.Contains(param) ||
                        a.Empresa.CNPJ.Contains(param) ||
                        a.Empresa.Nome_Empresarial.Contains(param) ||
                        a.Empresa.CNAE_Principal.Contains(param) ||
                        a.Empresa.Atividade_Principal.Contains(param) ||
                        a.Servicos.Contains(param) ||
                        a.Setor.Contains(param) ||
                        a.Owner_AppUser_Id.Contains(param))
                        .AsNoTrackingWithIdentityResolution()
                        .OrderBy(o => o.Data)
                        .ToListAsync();     
        }

        public async Task<IEnumerable<EAtendimento>> DoListAsync(Expression<Func<EAtendimento, bool>> filter = null)
        {
            var _query = _db.Atendimento.AsQueryable();

            if(filter != null)
                _query = _query
                    .Where(filter)
                    .Where(a => a.Status == "Finalizado")
                    .Where(a => a.Ativo == true)
                    .Include(p => p.Pessoa)
                    .Include(e => e.Empresa)
                    .Include(s => s.Sebrae)
                    .OrderBy(o => o.Data)
                    .AsNoTrackingWithIdentityResolution();

            return await _query.ToListAsync();
        }

        public async Task<IEnumerable<EAtendimento>> DoListByAnoAsync(int ano)
        {
            return await _db.Atendimento
                        .Include(p => p.Pessoa)
                        .Include(e => e.Empresa)
                        .Include(s => s.Sebrae)
                        .Where(a => a.Data.Value.Date.Year == ano &&
                                    a.Status == "Finalizado" && 
                                    a.Ativo == true)
                        .AsNoTrackingWithIdentityResolution()
                        .OrderBy(o => o.Data)
                        .ToListAsync();
        }

        public async Task<EAtendimento> GetAtendimentoAsync(Guid id)
        {
            var ativo = Task.Run(() => _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(i => i.Id == id).FirstOrDefault());

            return await ativo;
        }

        public Task<EAtendimento> GetIdAsync(Guid id)
        {
            return Task.Run(() => _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(i => i.Id == id).FirstOrDefault());
        }

        public async Task<IEnumerable<EAtendimento>> ListAllAsync()
        {
            return await _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .OrderByDescending(o => o.Data.Value.Date)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<IEnumerable<EAtendimento>> ListAtendimentoAtivoAsync(string userid)
        {
            var ativo = Task.Run(()=> _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(s => s.Owner_AppUser_Id == userid && s.Status == "Ativo"));

            return await ativo;
        }

        public async Task<IEnumerable<EAtendimento>> ListAtendimentosAtivosAsync()
        {
            var ativo = Task.Run(() => _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(s => s.Status == "Ativo"));

            return await ativo;
        }

        public async Task<IEnumerable<EAtendimento>> ListAtendimentosCanceladosAsync(string userid)
        {
            var ativo = Task.Run(()=> _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(s => s.Owner_AppUser_Id == userid && s.Status == "Cancelado" && s.Ativo == true).OrderBy(o => o.DataF));

            return await ativo;
        }

        public async Task<IEnumerable<EAtendimento>> ListCanalAsync(string canal)
        {
            return await Task.Run(() => _db.Atendimento.Where(u => u.Canal.Contains(canal)));
        }

        public async Task<IEnumerable<EAtendimento>> ListDateAsync(DateTime? dateTime)
        {
            return await Task.Run(() => _db.Atendimento.Where(u => u.Data == dateTime));
        }

        public async Task<IEnumerable<EAtendimento>> ListEmpresaAsync(string cnpj)
        {
            return await Task.Run(() => _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(u => u.Empresa.CNPJ == cnpj).OrderBy(d => d.Data).OrderByDescending(o => o.Data));
        }

        public async Task<IEnumerable<EAtendimento>> ListMeusAtendimentosAsync(string userid, DateTime? date)
        {
            return await Task.Run(() => _db.Atendimento
                .Include(u => u.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(a => a.Owner_AppUser_Id == userid && a.Data.Value.Date == date.Value.Date && a.Status == "Finalizado" && a.Ativo == true)
                .OrderBy(o => o.Data)
                .AsNoTrackingWithIdentityResolution());
        }

        public async Task<IEnumerable<EAtendimento>> ListMeusAtendimentosRaeAsync(string userid)
        {
            return await Task.Run(() => _db.Atendimento
                .Include(u => u.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(a => a.Owner_AppUser_Id == userid && a.Status == "Finalizado" && a.Ativo == true)
                .OrderBy(o => o.Data)
                .AsNoTrackingWithIdentityResolution());
        }

        public async Task<IEnumerable<EAtendimento>> ListMonthAsync(DateTime? month)
        {
            return await Task.Run(() => _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(a => a.Data.Value.Month == month.Value.Month
                && a.Status == "Finalizado"
                && a.Ativo == true)
                .OrderBy(o => o.Data)
                .AsNoTrackingWithIdentityResolution());
        }

        public async Task<IEnumerable<EAtendimento>> ListParamAsync(List<object> lparam)
        {
            var d1 = lparam[0].ToString();
            var d2 = lparam[1].ToString();
            d1.Replace("-", "/");
            d2.Replace("-", "/");

            var dataI = Convert.ToDateTime(d1);
            var dataF = Convert.ToDateTime(d2);
            var cpf = lparam[2] != null ? (string)lparam[2] : "";
            var nome = lparam[3] != null ? (string)lparam[3] : "";
            var cnpj = lparam[4] != null ? (string)lparam[4] : "";
            var razaosocial = lparam[5] != null ? (string)lparam[5] : "";
            var cnae = lparam[6] != null ? (string)lparam[6] : "";
            var servico = lparam[7] != null ? (string)lparam[7] : "";
            var user = lparam[8] != null ? (string)lparam[8] : "";


            return await _db.Atendimento
            .Include(p => p.Pessoa)
            .Include(e => e.Empresa)
            .Include(s => s.Sebrae)
            .Where(a => a.Data.Value.Date >= dataI && a.Data.Value.Date <= dataF)
            .Where(a => a.Status == "Finalizado" && a.Ativo == true)
            .Where(a => a.Pessoa.CPF.Contains(cpf))
            .Where(a => a.Pessoa.Nome.Contains(nome))
            .Where(a => a.Empresa.CNPJ.Contains(cnpj))
            .Where(a => a.Empresa.Nome_Empresarial.Contains(razaosocial))
            .Where(a => a.Empresa.CNAE_Principal.Contains(cnae))
            .Where(a => a.Servicos.Contains(servico))
            .Where(a => a.Owner_AppUser_Id.Contains(user))
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(o => o.Data)
            .ToListAsync();             
        }

        public async Task<IEnumerable<EAtendimento>> ListPeriodoAsync(DateTime? dataI, DateTime? dataF)
        {
            return await Task.Run(() => _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(a => a.Data.Value.Date >= dataI
                && a.Data.Value.Date <= dataF && a.Status == "Finalizado" && a.Ativo == true)
                .OrderBy(o => o.Data)
                .AsNoTrackingWithIdentityResolution());
        }

        public async Task<IEnumerable<EAtendimento>> ListPessoaAsync(string cpf)
        {
            return await Task.Run(() => _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(u => u.Pessoa.CPF == cpf).OrderBy(d => d.Data)
                .OrderByDescending(o => o.Data)
                .AsNoTrackingWithIdentityResolution());
        }

        public async Task<IEnumerable<EAtendimento>> ListServicosAsync(string servicos)
        {
            return await Task.Run(() => _db.Atendimento.Where(u => u.Servicos.Contains(servicos) && u.Status == "Finalizado" && u.Ativo == true)
                                            .AsNoTrackingWithIdentityResolution());
        }

        public async Task<IEnumerable<EAtendimento>> ListSetorAsync(string setor)
        {
            return await Task.Run(() => _db.Atendimento.Where(u => u.Setor == setor && u.Status == "Finalizado" && u.Ativo == true)
                                            .AsNoTrackingWithIdentityResolution());
        }

        public async Task<IEnumerable<EAtendimento>> ListUserNameAsync(string username)
        {
            return await Task.Run(() => _db.Atendimento.Where(u => u.Owner_AppUser_Id == username && u.Status == "Finalizado" && u.Ativo == true)
                                            .AsNoTrackingWithIdentityResolution());
        }

        public async Task<IEnumerable<EAtendimento>> ListUserNamePeriodoAsync(string username, DateTime? date)
        {
            return await Task.Run(() => _db.Atendimento
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .Include(s => s.Sebrae)
                .Where(u => u.Owner_AppUser_Id == username && u.Status == "Finalizado" && u.Ativo == true && u.Data.Value.Date == date.Value.Date)
                .AsNoTrackingWithIdentityResolution());
        }
    }
}
