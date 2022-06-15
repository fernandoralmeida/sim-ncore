using Sim.Data.Cnpj.Context;
using Sim.Domain.Cnpj.Entity;
using Sim.Domain.Cnpj.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Sim.Data.Cnpj.Repository
{
    public class RepositoryCnpj : RepositoryBase<BaseReceitaFederal>, IRepositoryCnpj
    {
        
        public RepositoryCnpj(ApplicationContext applicationContext)
            :base(applicationContext)
        {

        }

        public Task<BaseReceitaFederal> GetCNPJAsync(string razaosocial)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BaseReceitaFederal>> ListAllAsync(string endereco, string cnae, string municipio, string situacaocadastral)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BaseReceitaFederal>> ListAllAsync(string situacaocadastral)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BaseReceitaFederal>> ListAllMatrizFilialAsync(string cnpjbase)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BaseReceitaFederal>> ListAllRazaoSocialAsync(string razaosocial)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BaseReceitaFederal>> ListAllSocioAsync(string nomesocio)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BaseReceitaFederal>> ListEnderecoCnaeAsync(string endereco, string cnae, string municipio, string situacaocadastral)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BaseReceitaFederal>> ListOptantesSimplesNacionalAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BaseReceitaFederal>> ListOptantesSimplesNacionalAsync(string endereco, string cnae, string municipio, string situacaocadastral)
        {
            throw new NotImplementedException();
        }
    }
}
