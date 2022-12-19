using Sim.Domain.Cnpj.Entity;
using Sim.Domain.Cnpj.Interfaces;
using Sim.Application.Cnpj.Interfaces;
using System.Linq.Expressions;

namespace Sim.Application.Cnpj.Services
{
    public class AppServiceCnpj : AppServiceBase<BaseReceitaFederal>, IAppServiceCnpj
    {
        private readonly IServiceCnpj  _cnpj;

        public AppServiceCnpj(IServiceCnpj cnpj):base(cnpj)
        {
            _cnpj = cnpj;   
        }

        public async Task<IEnumerable<BaseReceitaFederal>> DoListBaseRazaoSociosAsync(string param) =>
            await _cnpj.DoListBaseRazaoSociosAsync(param);

        public async Task<IEnumerable<BICnae>> DoListBICnaeAsync(string municipio) =>
            await _cnpj.DoListBICnaeAsync(municipio);

        public async Task<IEnumerable<BIEmpresas>> DoListBIEmpresasAsync(string municipio, string situacao, string ano, string mes) =>
            await _cnpj.DoListBIEmpresasAsync(municipio, situacao, ano, mes);

        public async Task<IEnumerable<BaseReceitaFederal>> DoListEmpresasAsync(string municipio) =>
            await _cnpj.DoListEmpresasAsync(municipio);

        public async Task<IEnumerable<KeyValuePair<string, int>>> DoListMappingLogradourosAsync(string zona, string municipio, string situacao) {
            var _r = await DoListEmpresasAsync(municipio);            
            return await _cnpj.DoListMappingLogradourosAsync(_r.Where(s => s.Estabelecimento.Bairro == zona && s.Estabelecimento.SituacaoCadastral == situacao));
        }      

        public async Task<IEnumerable<string>> DoListMappingZonasAsync(string municipio, string situacao) {            
            var _r = await DoListEmpresasAsync(municipio);            
            return await _cnpj.DoListMappingZonasAsync(_r.Where(s => s.Estabelecimento.SituacaoCadastral == situacao));  
        }          

        public async Task<IEnumerable<Municipio>> DoListMicroRegiaoJahuAsync() =>
            await _cnpj.DoListMicroRegiaoJahuAsync();

        public async Task<IEnumerable<Municipio>> DoListMinicipiosAsync() =>
            await _cnpj.DoListMinicipiosAsync();

        public async Task<IEnumerable<(string Cnpj, string RazaoSocial, string Tel, string Email, string Cnae)>> DoListCnaeEmpresasJsonAsync(string cnaei, string cnaef, string municipio, string situacao) =>
            await _cnpj.DoListCnaeEmpresasJsonAsync(cnaei, cnaef, municipio, situacao);

        public async Task<IEnumerable<ELocalizacao>> DoListZonaJsonAsync(string zona, string municipio, string situacao) =>
            await _cnpj.DoListZonaJsonAsync(zona, municipio, situacao);
        public async Task<IEnumerable<ELocalizacao>> DoListLogradouroJsonAsync(string logradouro, string municipio, string situacao) =>
            await _cnpj.DoListLogradouroJsonAsync(logradouro, municipio, situacao);

        public async Task<IEnumerable<BaseReceitaFederal>> DoListByZonaAsync(string zona, string municipio, string situacao) => 
            await _cnpj.DoListByZonaAsync(zona, municipio);
        
        public async Task<IEnumerable<BaseReceitaFederal>> DoListByLogradouroAsync(string logradouro, string municipio, string situacao) => 
            await _cnpj.DoListByLogradouroAsync(logradouro, municipio);

        public async Task<IEnumerable<EExport>> DoListExport(string municipio) =>
            await _cnpj.DoListExport(await DoListAsync());

        public async Task<BaseReceitaFederal> GetCNPJAsync(string cnpj) =>
            await _cnpj.GetCNPJAsync(cnpj);

        public async Task<IEnumerable<BaseReceitaFederal>> DoListAsync(Expression<Func<BaseReceitaFederal, bool>> filter = null) =>
            await _cnpj.DoListAsync(filter);
    }
}
