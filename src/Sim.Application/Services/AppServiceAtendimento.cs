using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{

    public class AppServiceAtendimento : AppServiceBase<Atendimento>, IAppServiceAtendimento
    {
        private readonly IServiceAtendimento _atendimento;
        public AppServiceAtendimento(IServiceAtendimento atendimento)
            : base(atendimento)
        {
            _atendimento = atendimento;
        }

        public async Task<Atendimento> GetAtendimentoAsync(Guid id)
        {
            return await _atendimento.GetAtendimentoAsync(id);
        }

        public async Task<Atendimento> GetIdAsync(Guid id)
        {
            return await _atendimento.GetIdAsync(id);
        }

        public async Task<IEnumerable<Atendimento>> ListAllAsync()
        {
            return await _atendimento.ListAllAsync();
        }

        public async Task<IEnumerable<Atendimento>> ListAtendimentoAtivoAsync(string userid)
        {
            return await _atendimento.ListAtendimentoAtivoAsync(userid);
        }

        public async Task<IEnumerable<Atendimento>> ListAtendimentosAtivosAsync()
        {
            return await _atendimento.ListAtendimentosAtivosAsync();
        }

        public async Task<IEnumerable<Atendimento>> ListAtendimentosCanceladosAsync(string userid)
        {
            return await _atendimento.ListAtendimentosCanceladosAsync(userid);
        }

        public async Task<IEnumerable<Atendimento>> ListCanalAsync(string canal)
        {
            return await _atendimento.ListCanalAsync(canal);
        }

        public async Task<IEnumerable<Atendimento>> ListDateAsync(DateTime? dateTime)
        {
            return await _atendimento.ListDateAsync(dateTime);
        }

        public async Task<IEnumerable<Atendimento>> ListEmpresaAsync(string cnpj)
        {
            return await _atendimento.ListEmpresaAsync(cnpj);
        }

        public async Task<IEnumerable<Atendimento>> ListMeusAtendimentosAsync(string userid, DateTime? date)
        {
            return await _atendimento.ListMeusAtendimentosAsync(userid, date);
        }

        public async Task<IEnumerable<Atendimento>> ListMeusAtendimentosRaeAsync(string userid)
        {
            return await _atendimento.ListMeusAtendimentosRaeAsync(userid);
        }

        public async Task<IEnumerable<Atendimento>> ListMonthAsync(DateTime? month)
        {
            return await _atendimento.ListMonthAsync(month);
        }

        public async Task<IEnumerable<Atendimento>> ListParamAsync(List<object> lparam)
        {
            return await _atendimento.ListParamAsync(lparam);
        }

        public async Task<IEnumerable<Atendimento>> ListPeriodoAsync(DateTime? dataI, DateTime? dataF)
        {
            return await _atendimento.ListPeriodoAsync(dataI, dataF);   
        }

        public async Task<IEnumerable<Atendimento>> ListPessoaAsync(string cpf)
        {
            return await _atendimento.ListPessoaAsync(cpf);
        }

        public async Task<IEnumerable<Atendimento>> ListRaeLancadosAsync(string username)
        {
            return await _atendimento.ListRaeLancadosAsync(await ListMeusAtendimentosRaeAsync(username));
        }

        public async Task<IEnumerable<Atendimento>> ListRaeNaoLancadosAsync(string username)
        {
            return await _atendimento.ListRaeNaoLancadosAsync(await ListMeusAtendimentosRaeAsync(username));
        }

        public async Task<IEnumerable<Atendimento>> ListServicosAsync(string servicos)
        {
            return await _atendimento.ListServicosAsync(servicos);
        }

        public async Task<IEnumerable<Atendimento>> ListSetorAsync(string setor)
        {
            return await _atendimento.ListSetorAsync(setor);
        }

        public async Task<IEnumerable<Atendimento>> ListUserNameAsync(string username)
        {
            return await _atendimento.ListUserNameAsync(username);
        }

        public async Task<IEnumerable<Atendimento>> ListUserNamePeriodoAsync(string username, DateTime? date)
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
