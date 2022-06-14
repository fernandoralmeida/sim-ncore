using Sim.Domain.Cnpj.Entity;
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
    }
}
