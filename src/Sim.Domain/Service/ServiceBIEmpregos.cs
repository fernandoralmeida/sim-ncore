using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Domain.Interface.IRepository;

namespace Sim.Domain.Service;
public class ServiceBIEmpregos : IServiceBIEmpregos
{
    private readonly IRepositoryEmpregos _repositoryEmpregos; 
    public ServiceBIEmpregos(IRepositoryEmpregos repositoryEmpregos) {
        _repositoryEmpregos = repositoryEmpregos;        
    }
    public async Task<KeyValuePair<string, int>> DoEmpregosAtivos(int ano)
    {
        return await Task.Run(async () => {
            var vagas = 0;

            foreach(var item in await _repositoryEmpregos.DoListEmpregosAsyncByAno(ano))
            {
                if(item.Status == "Ativo")
                    vagas += item.Vagas;               
            }

            return new KeyValuePair<string, int>("Vaga(as) Disponível(eis)", vagas);
          });
    }    

    public async Task<KeyValuePair<string, int>> DoEmpregosAtivosAcumulado(int ano)
    {
        return await Task.Run(async () => {
            var vagas = 0;

            foreach(var item in await _repositoryEmpregos.DoListEmpregosAsyncByAno(ano))
            {
                vagas += item.Vagas;               
            }

            return new KeyValuePair<string, int>("Vaga(as) Acumulada(as)", vagas);
          });
    }

    public async Task<KeyValuePair<string, int>> DoEmpregosFinalizados(int ano)
    {
        return await Task.Run(async () => {
            var vagas = 0;

            foreach(var item in await _repositoryEmpregos.DoListEmpregosAsyncByAno(ano))
            {
                if(item.Status == "Finalizado")
                    vagas += item.Vagas;               
            }

            return new KeyValuePair<string, int>("Vaga(as) Disponível(eis)", vagas);
          });
    }

    public async Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByGenero(int ano)
    {
        return await Task.Run(async () => {
            int vagas = 0;
            int vagas_f = 0;
            int vagas_m = 0;

            var _return = new List<KeyValuePair<string, int>>();

            foreach(var item in await _repositoryEmpregos.DoListEmpregosAsyncByAno(ano))
            {
                if(item.Status == "Ativo")
                    switch(item.Genero)
                    {
                        case("Neutro"):
                            vagas += item.Vagas;
                            break;

                        case("Masculino"):
                            vagas_m += item.Vagas;
                            break;

                        case("Feminino"):
                            vagas_f += item.Vagas;
                            break;
                    }            
            }

            _return.Add(new KeyValuePair<string, int>("Neutro", vagas));
            _return.Add(new KeyValuePair<string, int>("Masculino", vagas_m));
            _return.Add(new KeyValuePair<string, int>("Feminino", vagas_f));

            return _return.OrderBy(o => o.Value);
          });
    }

    public async Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByGeneroAcumulado(int ano)
    {
        return await Task.Run(async () => {
            int vagas = 0;
            int vagas_f = 0;
            int vagas_m = 0;

            var _return = new List<KeyValuePair<string, int>>();

            foreach(var item in await _repositoryEmpregos.DoListEmpregosAsyncByAno(ano))
            {
                switch(item.Genero)
                {
                    case("Neutro"):
                        vagas += item.Vagas;
                        break;

                    case("Masculino"):
                        vagas_m += item.Vagas;
                        break;

                    case("Feminino"):
                        vagas_f += item.Vagas;
                        break;
                }            
            }

            _return.Add(new KeyValuePair<string, int>("Neutro", vagas));
            _return.Add(new KeyValuePair<string, int>("Masculino", vagas_m));
            _return.Add(new KeyValuePair<string, int>("Feminino", vagas_f));

            return _return.OrderBy(o => o.Value);
          });
    }

    public async Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByInclusao(int ano)
    {
        return await Task.Run(() => {
            var _return = new List<KeyValuePair<string, int>>();


            return _return;
          });
    }

    public async Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByInclusaoAcumulado(int ano)
    {
        return await Task.Run(() => {
            var _return = new List<KeyValuePair<string, int>>();


            return _return;
          });
    }

    public async Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByTipo(int ano)
    {
        return await Task.Run(() => {
            var _return = new List<KeyValuePair<string, int>>();


            return _return;
          });
    }

    public async Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByTipoAcumulado(int ano)
    {
        return await Task.Run(() => {
            var _return = new List<KeyValuePair<string, int>>();


            return _return;
          });
    }
}