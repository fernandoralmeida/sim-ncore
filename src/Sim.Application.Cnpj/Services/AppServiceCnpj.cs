using Sim.Domain.Cnpj.Entity;
using Sim.Domain.Cnpj.Interfaces;
using Sim.Application.Cnpj.Interfaces;
using System.Linq.Expressions;

namespace Sim.Application.Cnpj.Services
{
    public class AppServiceCnpj : AppServiceBase<BaseReceitaFederal>, IAppServiceCnpj
    {
        private readonly IServiceCnpj _cnpj;

        public AppServiceCnpj(IServiceCnpj cnpj) : base(cnpj)
        {
            _cnpj = cnpj;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> DoListBaseRazaoSociosAsync(string param) =>
            await _cnpj.DoListBaseRazaoSociosAsync(param);

        public async Task<IEnumerable<BICnae>> DoListBICnaeAsync(IEnumerable<BaseReceitaFederal> param) =>
            await _cnpj.DoListBICnaeAsync(param);

        public async Task<IEnumerable<BIEmpresas>> DoListBIEmpresasAsync(string municipio, int ano) =>
            await _cnpj.DoListBIEmpresasAsync(municipio, ano);

        public async Task<IEnumerable<BaseReceitaFederal>> DoListEmpresasAsync(string municipio) =>
            await _cnpj.DoListEmpresasAsync(municipio);

        public async Task<IEnumerable<KeyValuePair<string, int>>> DoListMappingLogradourosAsync(string zona, string municipio, string situacao)
        {
            var _r = await DoListEmpresasAsync(municipio);
            return await _cnpj.DoListMappingLogradourosAsync(_r.Where(s => s.Estabelecimento.Bairro == zona && s.Estabelecimento.SituacaoCadastral == situacao));
        }

        public async Task<IEnumerable<string>> DoListMappingZonasAsync(string municipio, string situacao)
        {
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

        public async Task<IEnumerable<BaseReceitaFederal>> DoListByLogradouroAsync(string logradouro, string municipio, string situacao)
        {
            var _list = await _cnpj.DoListByLogradouroAsync(logradouro, municipio);
            return _list.Where(s => s.Estabelecimento.SituacaoCadastral == situacao);
        }

        public async Task<IEnumerable<EExport>> DoListExport(string municipio) =>
            await _cnpj.DoListExport(await DoListAsync(s => s.Municipio == municipio && s.SituacaoCadastral == "02"));

        public async Task<BaseReceitaFederal> GetCNPJAsync(string cnpj) =>
            await _cnpj.GetCNPJAsync(cnpj);

        public async Task<IEnumerable<BaseReceitaFederal>> DoListAsync(Expression<Func<Estabelecimento, bool>>? filter = null) =>
            await _cnpj.DoListAsync(filter);

        public async Task<IEnumerable<(int Value, string Key, string Code)>> DoListCnaesAsync(Expression<Func<Estabelecimento, bool>> filter = null)
            => await _cnpj.DoListCnaesAsync(filter);

        public async Task<IEnumerable<BaseReceitaFederal>> DoListCNAEAsync(string municipio, Expression<Func<CNAE, bool>>? param = null)
            => await _cnpj.DoListCNAEAsync(municipio, param);

        public async Task<IEnumerable<KeyValuePair<string, int>>> DoMappingLogradourosAsync(string logradouro, string municipio, string situacao)
        {
            var _r = await _cnpj.DoListAsync(
                s => s.Municipio == municipio &&
                s.SituacaoCadastral == situacao &&
                s.Logradouro.Contains(logradouro));

            return await _cnpj.DoListMappingLogradourosAsync(_r);
        }

        public async Task<IEnumerable<KeyValuePair<string, int>>> DoMappingLogradourosByZonaAsync(string zona, string municipio, string situacao)
        {
            var _r = await _cnpj.DoListAsync(
                s => s.Municipio == municipio &&
                s.SituacaoCadastral == situacao &&
                s.Bairro.Contains(zona));

            return await _cnpj.DoListMappingLogradourosAsync(_r);
        }
    }
}
