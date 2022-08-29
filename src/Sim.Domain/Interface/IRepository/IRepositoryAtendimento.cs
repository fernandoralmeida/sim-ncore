namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositoryAtendimento : IRepositoryBase<Atendimento>
    {
        Task<Atendimento> GetAtendimentoAsync(Guid id);
        Task<IEnumerable<Atendimento>> DoListByAnoAsync(int ano);
        Task<IEnumerable<Atendimento>> DoListAendimentosAsyncBy(string param);
        Task<IEnumerable<Atendimento>> ListPessoaAsync(string cpf);
        Task<IEnumerable<Atendimento>> ListEmpresaAsync(string cnpj);
        Task<IEnumerable<Atendimento>> ListSetorAsync(string setor);
        Task<IEnumerable<Atendimento>> ListCanalAsync(string canal);
        Task<IEnumerable<Atendimento>> ListServicosAsync(string servicos);
        Task<IEnumerable<Atendimento>> ListDateAsync(DateTime? dateTime);
        Task<IEnumerable<Atendimento>> ListMeusAtendimentosAsync(string userid, DateTime? date);
        Task<IEnumerable<Atendimento>> ListMeusAtendimentosRaeAsync(string userid);
        Task<IEnumerable<Atendimento>> ListAtendimentoAtivoAsync(string userid);
        Task<IEnumerable<Atendimento>> ListAtendimentosCanceladosAsync(string userid);
        Task<IEnumerable<Atendimento>> ListPeriodoAsync(DateTime? dataI, DateTime? dataF);
        Task<IEnumerable<Atendimento>> ListMonthAsync(DateTime? month);        
        Task<IEnumerable<Atendimento>> ListUserNameAsync(string username);
        Task<IEnumerable<Atendimento>> ListUserNamePeriodoAsync(string username, DateTime? date);
        Task<IEnumerable<Atendimento>> ListAtendimentosAtivosAsync();
        Task<IEnumerable<Atendimento>> ListParamAsync(List<object> lparam);
        Task<Atendimento> GetIdAsync(Guid id);
        Task<IEnumerable<Atendimento>> ListAllAsync();
    }
}
