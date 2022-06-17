using Sim.Domain.Cnpj.Entity;
using Sim.Domain.Cnpj.Interfaces;
using Sim.Application.Cnpj.Interfaces;

namespace Sim.Application.Cnpj.Services
{
    public class AppServiceCnpj : AppServiceBase<BaseReceitaFederal>, IAppServiceCnpj
    {
        private readonly IServiceCnpj  _cnpj;

        public AppServiceCnpj(IServiceCnpj cnpj):base(cnpj)
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

        public async Task<IEnumerable<BICnae>> ToListBICnaeAsync(string municipio)
        {
            return await _cnpj.ToListBICnaeAsync(municipio);
        }

        public async Task<IEnumerable<BIEmpresas>> ToListBIEmpresasAsync(string municipio, string situacao, string ano, string mes)
        {
            return await _cnpj.ToListBIEmpresasAsync(municipio, situacao, ano, mes);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ToListByCnaeAsync(string atividade, string municipio)
        {
            return await _cnpj.ToListByCnaeAsync(atividade, municipio);
        }

        public async Task<IEnumerable<(string Cnpj, string RazaoSocial, string Tel, string Email)>> ToListCnaeEmpresasJsonAsync(string cnae, string municipio, string situacao)
        {
            return await _cnpj.ToListCnaeEmpresasJsonAsync(cnae, municipio, situacao);
        }

        public async Task<IEnumerable<Municipio>> ToListMicroRegiaoJahuAsync()
        {
            return await _cnpj.ToListMicroRegiaoJahuAsync();
        }

        public async Task<IEnumerable<Municipio>> ToListMinicipiosAsync()
        {
            return await _cnpj.ToListMinicipiosAsync();
        }
    }
}
