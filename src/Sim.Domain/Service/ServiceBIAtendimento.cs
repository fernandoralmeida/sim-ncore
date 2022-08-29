using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service;

public class ServiceBIAtendimento : IServiceBIAtendimento {
    
    private readonly IRepositoryAtendimento _atendimento;
    public ServiceBIAtendimento(IRepositoryAtendimento repositoryAtendimento) {
        _atendimento = repositoryAtendimento;        
    }
    
    public async Task<EChartDual> DoListByAnoAsync(int ano)
    {
        return await Task.Run(async () => {
            var ato = 0;
            var src = 0;

            foreach(var item in await _atendimento.DoListByAnoAsync(ano))
            {                
                if(item.Servicos != null)
                {
                    string[] servicos = item.Servicos.ToString().Split(new char[] { ';', ',' });
                    ato ++;
                    foreach(var s in servicos) {
                        src ++;
                    }
                }
            }
            return new EChartDual("Atendimentos", ato, src);
          });
    }

    public Task<IEnumerable<EChart>> DoListByClienteAsync(int ano)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<EChart>> DoListByService(int ano)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<EChart>> DoListBySetorAsync(int ano)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<EChart>> DoListByUserAsync(int ano)
    {
        throw new NotImplementedException();
    }
}