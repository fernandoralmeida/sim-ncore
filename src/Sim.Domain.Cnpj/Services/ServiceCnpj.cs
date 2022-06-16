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

        public async Task<BaseReceitaFederal> GetCNPJAsync(string cnpj)
        {
            return await _cnpj.GetCNPJAsync(cnpj);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAllAsync(string bairro, string endereco, string cnae, string municipio, string situacaocadastral)
        {
            return await _cnpj.ListAllAsync(bairro, endereco, cnae, municipio, situacaocadastral);
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

        public async Task<IEnumerable<BaseReceitaFederal>> ListOptantesSimplesNacionalAsync(string municipio, string situacaocadastral)
        {
            return await _cnpj.ListOptantesSimplesNacionalAsync(municipio, situacaocadastral);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListOptantesSimplesNacionalAsync(string endereco, string cnae, string municipio, string situacaocadastral)
        {
            return await _cnpj.ListOptantesSimplesNacionalAsync(endereco, cnae, municipio, situacaocadastral);
        }
    }
}
