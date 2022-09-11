using Sim.Domain.Cnpj.Entity;

namespace Sim.Domain.Cnpj.Interfaces
{
    public interface IServiceCnpj : IServiceBase<BaseReceitaFederal>
    {
        Task<IEnumerable<BaseReceitaFederal>> DoListBaseRazaoSociosAsync(string param);
        Task<IEnumerable<BaseReceitaFederal>> DoListEmpresasAsync(string municipio);
        Task<IEnumerable<Municipio>> DoListMinicipiosAsync();
        Task<IEnumerable<Municipio>> DoListMicroRegiaoJahuAsync();
        Task<IEnumerable<BICnae>> DoListBICnaeAsync(string municipio);
        Task<IEnumerable<BIEmpresas>> DoListBIEmpresasAsync(string municipio, string situacao, string ano, string mes);
        Task<IEnumerable<BaseReceitaFederal>> DoListByZonaAsync(string zona, string municipio);
        Task<IEnumerable<BaseReceitaFederal>> DoListByLogradouroAsync(string logradouro, string municipio);
        Task<IEnumerable<(string Cnpj, string RazaoSocial, string Tel, string Email, string Cnae)>> DoListCnaeEmpresasJsonAsync(string cnaei, string cnaef, string municipio, string situacao);
        Task<IEnumerable<ELocalizacao>> DoListZonaJsonAsync(string zona, string municipio, string situacao);
        Task<IEnumerable<ELocalizacao>> DoListLogradouroJsonAsync(string logradouro, string municipio, string situacao);
        Task<IEnumerable<KeyValuePair<string, int>>> DoListMappingLogradourosAsync(IEnumerable<BaseReceitaFederal> obj);
        Task<IEnumerable<string>> DoListMappingZonasAsync(IEnumerable<BaseReceitaFederal> obj);
    }
}
