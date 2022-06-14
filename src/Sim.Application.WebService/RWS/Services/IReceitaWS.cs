using Sim.Domain.WebService.RWS.Entity;

namespace Sim.Domain.WebService.RWS.Services
{
    public interface IReceitaWS
    {
        CNPJ ConsultarCPNJ(string cnpj);
        Task<CNPJ> ConsultarCPNJAsync(string cnpj);
    }
}
