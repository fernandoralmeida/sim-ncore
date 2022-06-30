﻿using Sim.Domain.Cnpj.Entity;

namespace Sim.Domain.Cnpj.Interfaces
{
    public interface IServiceCnpj : IServiceBase<BaseReceitaFederal>
    {
        Task<IEnumerable<BaseReceitaFederal>> ListAllAsync(string situacaocadastral);
        Task<IEnumerable<BaseReceitaFederal>> ListAllAsync(string bairro, string endereco, string cnae, string municipio, string situacaocadastral);
        Task<IEnumerable<BaseReceitaFederal>> ListOptantesSimplesNacionalAsync(string municipio, string situacaocadastral);
        Task<IEnumerable<BaseReceitaFederal>> ListOptantesSimplesNacionalAsync(string endereco, string cnae, string municipio, string situacaocadastral);
        Task<IEnumerable<BaseReceitaFederal>> ListAllSocioAsync(string nomesocio);
        Task<IEnumerable<BaseReceitaFederal>> ListAllMatrizFilialAsync(string cnpjbase);
        Task<IEnumerable<BaseReceitaFederal>> ListAllRazaoSocialAsync(string razaosocial);
        Task<IEnumerable<BaseReceitaFederal>> ToListByCnaeAsync(string atividade, string municipio);
        Task<BaseReceitaFederal> GetCNPJAsync(string cnpj);

        Task<IEnumerable<Municipio>> ToListMinicipiosAsync();
        Task<IEnumerable<Municipio>> ToListMicroRegiaoJahuAsync();
        Task<IEnumerable<BICnae>> ToListBICnaeAsync(string municipio);
        Task<IEnumerable<BIEmpresas>> ToListBIEmpresasAsync(string municipio, string situacao, string ano, string mes);
        Task<IEnumerable<(string Cnpj, string RazaoSocial, string Tel, string Email)>> ToListCnaeEmpresasJsonAsync(string cnae, string municipio, string situacao);
    }
}