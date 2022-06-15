﻿using Sim.Domain.Cnpj.Entity;
using Sim.Domain.Cnpj.Interfaces;

namespace Sim.Domain.Cnpj.Services
{
    public class ServiceCnpj : ServiceBase<BaseReceitaFederal>, IServiceCnpj
    {
        private readonly IRepositoryCnpj _cnpj;

        public ServiceCnpj(IRepositoryCnpj cnpj):base(cnpj)
        {
            _cnpj = cnpj;   
        }

        public async Task<BaseReceitaFederal> GetCNPJAsync(string razaosocial)
        {
            return await _cnpj.GetCNPJAsync(razaosocial);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAllAsync(string endereco, string cnae, string municipio, string situacaocadastral)
        {
            return await _cnpj.ListAllAsync(endereco, cnae, municipio, situacaocadastral);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAllAsync(string situacaocadastral)
        {
            return await _cnpj.ListAllAsync(situacaocadastral);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAllMatrizFilialAsync(string cnpjbase)
        {
            return await _cnpj.ListAllRazaoSocialAsync(cnpjbase);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAllRazaoSocialAsync(string razaosocial)
        {
            return await _cnpj.ListAllRazaoSocialAsync(razaosocial);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAllSocioAsync(string nomesocio)
        {
            return await _cnpj.ListAllSocioAsync(nomesocio);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListEnderecoCnaeAsync(string endereco, string cnae, string municipio, string situacaocadastral)
        {
            return await _cnpj.ListEnderecoCnaeAsync(endereco, cnae, municipio, situacaocadastral); 
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListOptantesSimplesNacionalAsync()
        {
            return await _cnpj.ListOptantesSimplesNacionalAsync();
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListOptantesSimplesNacionalAsync(string endereco, string cnae, string municipio, string situacaocadastral)
        {
            return await _cnpj.ListOptantesSimplesNacionalAsync(endereco, cnae, municipio, situacaocadastral);
        }
    }
}
