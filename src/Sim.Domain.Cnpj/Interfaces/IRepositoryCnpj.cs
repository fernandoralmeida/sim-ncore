using System.Linq.Expressions;
using Sim.Domain.Cnpj.Entity;

namespace Sim.Domain.Cnpj.Interfaces
{
    public interface IRepositoryCnpj : IRepositoryBase<BaseReceitaFederal>
    {
        Task<BaseReceitaFederal> GetCNPJAsync(string cnpj);
        Task<IEnumerable<Municipio>> DoListMinicipiosAsync();
        Task<IEnumerable<BaseReceitaFederal>> DoListAsync(Expression<Func<Estabelecimento, bool>> filter = null);
        Task<IEnumerable<BaseReceitaFederal>> DoListBaseRazaoSociosAsync(string param);
        Task<IEnumerable<BaseReceitaFederal>> DoListEmpresasAsync(string municipio);
        Task<IEnumerable<BaseReceitaFederal>> DoListByCnaeAsync(string atividadei, string atividadef, string municipio);
        Task<IEnumerable<BaseReceitaFederal>> DoListByZonaAsync(string zona, string municipio);
        Task<IEnumerable<BaseReceitaFederal>> DoListByLogradouroAsync(string logradouro, string municipio);
        Task<IEnumerable<BaseReceitaFederal>> DoListCNAEAsync(string municipio, Expression<Func<CNAE, bool>> param = null);
        Task<IEnumerable<BaseReceitaFederal>> RepositoryEstabelecimento(Expression<Func<Estabelecimento, bool>> param = null);
    }
}
