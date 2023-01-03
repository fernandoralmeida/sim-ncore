using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;
using System.Linq.Expressions;

namespace Sim.Application.Services
{

    public class AppServiceAtendimento : AppServiceBase<EAtendimento>, IAppServiceAtendimento
    {
        private readonly IServiceAtendimento _atendimento;
        public AppServiceAtendimento(IServiceAtendimento atendimento)
            : base(atendimento)
        {
            _atendimento = atendimento;
        }

        public async Task<IEnumerable<EAtendimento>> DoListAendimentosAsyncBy(string param)
        {
            return await _atendimento.DoListAendimentosAsyncBy(param);
        }

        public async Task<IEnumerable<EAtendimento>> DoListAsync(Expression<Func<EAtendimento, bool>> filter = null)
        {
            return await _atendimento.DoListAsync(filter: filter);
        }

        public async Task<IEnumerable<EAtendimento>> DoListByAnoAsync(int ano)
        {
            return await _atendimento.DoListByAnoAsync(ano);
        }

        public async Task<EAtendimento> GetAtendimentoAsync(Guid id)
        {
            return await _atendimento.GetAtendimentoAsync(id);
        }

        public async Task<EAtendimento> GetIdAsync(Guid id)
        {
            return await _atendimento.GetIdAsync(id);
        }

        public async Task<IEnumerable<EAtendimento>> ListAllAsync()
        {
            return await _atendimento.ListAllAsync();
        }

        public async Task<IEnumerable<EAtendimento>> ListAtendimentoAtivoAsync(string userid)
        {
            return await _atendimento.ListAtendimentoAtivoAsync(userid);
        }

        public async Task<IEnumerable<EAtendimento>> ListAtendimentosAtivosAsync()
        {
            return await _atendimento.ListAtendimentosAtivosAsync();
        }

        public async Task<IEnumerable<EAtendimento>> ListAtendimentosCanceladosAsync(string userid)
        {
            return await _atendimento.ListAtendimentosCanceladosAsync(userid);
        }

        public async Task<IEnumerable<EAtendimento>> ListCanalAsync(string canal)
        {
            return await _atendimento.ListCanalAsync(canal);
        }

        public async Task<IEnumerable<EAtendimento>> ListDateAsync(DateTime? dateTime)
        {
            return await _atendimento.ListDateAsync(dateTime);
        }

        public async Task<IEnumerable<EAtendimento>> ListEmpresaAsync(string cnpj)
        {
            return await _atendimento.ListEmpresaAsync(cnpj);
        }

        public async Task<IEnumerable<EAtendimento>> ListMeusAtendimentosAsync(string userid, DateTime? date)
        {
            return await _atendimento.ListMeusAtendimentosAsync(userid, date);
        }

        public async Task<IEnumerable<EAtendimento>> ListMeusAtendimentosRaeAsync(string userid)
        {
            return await _atendimento.ListMeusAtendimentosRaeAsync(userid);
        }

        public async Task<IEnumerable<EAtendimento>> ListMonthAsync(DateTime? month)
        {
            return await _atendimento.ListMonthAsync(month);
        }

        public async Task<IEnumerable<EAtendimento>> ListParamAsync(List<object> lparam)
        {
            return await _atendimento.ListParamAsync(lparam);
        }

        public async Task<IEnumerable<EAtendimento>> ListPeriodoAsync(DateTime? dataI, DateTime? dataF)
        {
            return await _atendimento.ListPeriodoAsync(dataI, dataF);   
        }

        public async Task<IEnumerable<EAtendimento>> ListPessoaAsync(string cpf)
        {
            return await _atendimento.ListPessoaAsync(cpf);
        }

        public async Task<IEnumerable<EAtendimento>> ListRaeLancadosAsync(string username, int ano)
        {
            return await _atendimento.ListRaeLancadosAsync(await ListMeusAtendimentosRaeAsync(username), ano);
        }

        public async Task<IEnumerable<EAtendimento>> ListRaeNaoLancadosAsync(string username, int ano)
        {
            return await _atendimento.ListRaeNaoLancadosAsync(await ListMeusAtendimentosRaeAsync(username), ano);
        }

        public async Task<IEnumerable<EAtendimento>> ListServicosAsync(string servicos)
        {
            return await _atendimento.ListServicosAsync(servicos);
        }

        public async Task<IEnumerable<EAtendimento>> ListSetorAsync(string setor)
        {
            return await _atendimento.ListSetorAsync(setor);
        }

        public async Task<IEnumerable<EAtendimento>> ListUserNameAsync(string username)
        {
            return await _atendimento.ListUserNameAsync(username);
        }

        public async Task<IEnumerable<EAtendimento>> ListUserNamePeriodoAsync(string username, DateTime? date)
        {
            return await _atendimento.ListUserNamePeriodoAsync(username, date);
        }

        public async Task<BIAtendimentos> ToListBIAtendimentos(DateTime periodo)
        {
            return await _atendimento.ToListBIAtendimentos(periodo);
        }

        public async Task<BIAtendimentos> ToListBIAtendimentosAppUser(DateTime periodo)
        {
            return await _atendimento.ToListBIAtendimentosAppUser(periodo);
        }

        public async Task<BIAtendimentos> ToListBIAtendimentosSetor(DateTime periodo, string setor)
        {
            return await _atendimento.ToListBIAtendimentosSetor(periodo, setor);
        }
    }
}
