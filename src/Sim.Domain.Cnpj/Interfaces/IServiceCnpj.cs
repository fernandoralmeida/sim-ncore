﻿using Sim.Domain.Cnpj.Entity;

namespace Sim.Domain.Cnpj.Interfaces
{
    public interface IServiceCnpj : IServiceBase<BaseReceitaFederal>
    {
        Task<IEnumerable<BaseReceitaFederal>> ListAllAsync(string endereco, string cnae, string municipio, string situacaocadastral);
        Task<IEnumerable<BaseReceitaFederal>> ListEnderecoCnaeAsync(string endereco, string cnae, string municipio, string situacaocadastral);
        Task<IEnumerable<BaseReceitaFederal>> ListOptantesSimplesNacionalAsync();
        Task<IEnumerable<BaseReceitaFederal>> ListOptantesSimplesNacionalAsync(string endereco, string cnae, string municipio, string situacaocadastral);
        Task<IEnumerable<BaseReceitaFederal>> ListAllSocioAsync(string nomesocio);
        Task<IEnumerable<BaseReceitaFederal>> ListAllMatrizFilialAsync(string cnpjbase);
        Task<IEnumerable<BaseReceitaFederal>> ListAllRazaoSocialAsync(string razaosocial);
        Task<BaseReceitaFederal> GetCNPJAsync(string razaosocial);
    }
}
