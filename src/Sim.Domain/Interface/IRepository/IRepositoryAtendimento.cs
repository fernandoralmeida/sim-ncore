﻿namespace Sim.Domain.Interface.IRepository
{
    using System.Linq.Expressions;
    using Entity;
    public interface IRepositoryAtendimento : IRepositoryBase<EAtendimento>
    {
        Task<EAtendimento> GetAtendimentoAsync(Guid id);
        Task<IEnumerable<EAtendimento>> DoListAsync(Expression<Func<EAtendimento, bool>>? filter = null);
        Task<IEnumerable<EAtendimento>> DoListByAnoAsync(int ano);
        Task<IEnumerable<EAtendimento>> DoListAendimentosAsyncBy(string param);
        Task<IEnumerable<EAtendimento>> ListPessoaAsync(string cpf);
        Task<IEnumerable<EAtendimento>> ListEmpresaAsync(string cnpj);
        Task<IEnumerable<EAtendimento>> ListSetorAsync(string setor);
        Task<IEnumerable<EAtendimento>> ListCanalAsync(string canal);
        Task<IEnumerable<EAtendimento>> ListServicosAsync(string servicos);
        Task<IEnumerable<EAtendimento>> ListDateAsync(DateTime? dateTime);
        Task<IEnumerable<EAtendimento>> ListMeusAtendimentosAsync(string userid, DateTime? date);
        Task<IEnumerable<EAtendimento>> ListMeusAtendimentosRaeAsync(string userid);
        Task<IEnumerable<EAtendimento>> ListAtendimentoAtivoAsync(string userid);
        Task<IEnumerable<EAtendimento>> ListAtendimentosCanceladosAsync(string userid);
        Task<IEnumerable<EAtendimento>> ListPeriodoAsync(DateTime? dataI, DateTime? dataF);
        Task<IEnumerable<EAtendimento>> ListMonthAsync(DateTime? month);        
        Task<IEnumerable<EAtendimento>> ListUserNameAsync(string username);
        Task<IEnumerable<EAtendimento>> ListUserNamePeriodoAsync(string username, DateTime? date);
        Task<IEnumerable<EAtendimento>> ListAtendimentosAtivosAsync();
        Task<IEnumerable<EAtendimento>> ListParamAsync(List<object> lparam);
        Task<EAtendimento> GetIdAsync(Guid id);
        Task<IEnumerable<EAtendimento>> ListAllAsync();
    }
}
