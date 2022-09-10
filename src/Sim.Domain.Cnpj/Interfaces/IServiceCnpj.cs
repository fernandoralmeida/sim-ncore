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
        Task<IEnumerable<(string Cnpj, string RazaoSocial, string Tel, string Email, string Cnae)>> DoListCnaeEmpresasJsonAsync(string cnaei, string cnaef, string municipio, string situacao);
        Task<IEnumerable<EMapping>> DoListMappingEmpresasAsync(IEnumerable<BaseReceitaFederal> obj);
    }
}
