namespace Sim.Domain.Organizacao.Interfaces.Service
{
    using Model;
    public interface IServiceParceiro : IServiceBase<EParceiro>
    {
        Task<IEnumerable<EParceiro>> ListParceirosAsync(string owner);
        Task<EParceiro> GetIdAsync(Guid id);
        Task<IEnumerable<EParceiro>> ListAllAsync();
    }
}
