using Sim.Domain.Cnpj.Entity;

namespace Sim.Application.Cnpj.Interfaces
{
    public interface IAppServiceCnpj : IAppServiceBase<BaseReceitaFederal>
    {
        Task<BaseReceitaFederal> GetCNPJAsync(string cnpj);
        Task<IEnumerable<BaseReceitaFederal>> DoListBaseRazaoSociosAsync(string param);
        Task<IEnumerable<BaseReceitaFederal>> DoListEmpresasAsync(string municipio);
        Task<IEnumerable<Municipio>> DoListMinicipiosAsync();
        Task<IEnumerable<Municipio>> DoListMicroRegiaoJahuAsync();
        Task<IEnumerable<BICnae>> DoListBICnaeAsync(string municipio);
        Task<IEnumerable<BIEmpresas>> DoListBIEmpresasAsync(string municipio, string situacao, string ano, string mes);
        Task<IEnumerable<BaseReceitaFederal>> DoListByZonaAsync(string zona, string municipio, string situacao);
        Task<IEnumerable<BaseReceitaFederal>> DoListByLogradouroAsync(string logradouro, string municipio, string situacao);
        Task<IEnumerable<(string Cnpj, string RazaoSocial, string Tel, string Email, string Cnae)>> DoListCnaeEmpresasJsonAsync(string cnaei, string cnaef, string municipio, string situacao);
        Task<IEnumerable<ELocalizacao>> DoListZonaJsonAsync(string zona, string municipio, string situacao);
        Task<IEnumerable<ELocalizacao>> DoListLogradouroJsonAsync(string logradouro, string municipio, string situacao);
        Task<IEnumerable<KeyValuePair<string, int>>> DoListMappingLogradourosAsync(string zona, string municipio, string situacao);
        Task<IEnumerable<string>> DoListMappingZonasAsync(string municipio, string situacao);
        Task<IEnumerable<EExport>> DoListExport(string municipio);
    }
}
