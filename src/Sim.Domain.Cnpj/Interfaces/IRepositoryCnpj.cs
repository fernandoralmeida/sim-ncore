using Sim.Domain.Cnpj.Entity;

namespace Sim.Domain.Cnpj.Interfaces
{
    public interface IRepositoryCnpj : IRepositoryBase<BaseReceitaFederal>
    {
        Task<IEnumerable<BaseReceitaFederal>> DoListBaseRazaoSociosAsync(string param);
        Task<IEnumerable<BaseReceitaFederal>> ListAllAsync(string municipio);
        Task<IEnumerable<BaseReceitaFederal>> ListAllAsync(string municipio, string situacaocadastral);
        Task<IEnumerable<BaseReceitaFederal>> ListAllAsync(string bairro, string endereco, string cnae, string municipio, string situacaocadastral);
        Task<IEnumerable<BaseReceitaFederal>> ListOptantesSimplesNacionalAsync(string municipio, string situacaocadastral);
        Task<IEnumerable<BaseReceitaFederal>> ListOptantesSimplesNacionalAsync(string endereco, string cnae, string municipio, string situacaocadastral);
        Task<IEnumerable<BaseReceitaFederal>> ListAllSocioAsync(string nomesocio);
        Task<IEnumerable<BaseReceitaFederal>> ListAllMatrizFilialAsync(string cnpjbase);
        Task<IEnumerable<BaseReceitaFederal>> ListAllRazaoSocialAsync(string razaosocial);
        Task<IEnumerable<BaseReceitaFederal>> ToListByCnaeAsync(string atividadei, string atividadef, string municipio);
        Task<BaseReceitaFederal> GetCNPJAsync(string cnpj);
        Task<IEnumerable<Municipio>> ToListMinicipiosAsync();
        Task<IEnumerable<BaseReceitaFederal>> DoListByAsync(string municipio);
    }
}
