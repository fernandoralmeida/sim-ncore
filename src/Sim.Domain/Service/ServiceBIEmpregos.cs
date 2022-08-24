using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Domain.Interface.IRepository;

namespace Sim.Domain.Service;
public class ServiceBIEmpregos : IServiceBIEmpregos
{
    private readonly IRepositoryEmpregos _repositoryEmpregos; 
    private IEnumerable<Empregos> _datalist;
    public ServiceBIEmpregos(IRepositoryEmpregos repositoryEmpregos) {
        _repositoryEmpregos = repositoryEmpregos;
    }
    public async Task<KeyValuePair<string, int>> DoEmpregosAtivos(int ano)
    {
        _datalist = await _repositoryEmpregos.DoListEmpregosAsyncByAno(ano);

        return await Task.Run(() => {
            var vagas = 0;

            foreach(var item in _datalist)
            {
                if(item.Status == "Ativo")
                    vagas += item.Vagas;               
            }

            return new KeyValuePair<string, int>("Disponível", vagas);
          });
    }    

    public async Task<KeyValuePair<string, int>> DoEmpregosAtivosAcumulado(int ano)
    {
        return await Task.Run(() => {
            var vagas = 0;

            foreach(var item in _datalist)
            {
                vagas += item.Vagas;               
            }

            return new KeyValuePair<string, int>("Acumulado", vagas);
          });
    }

    public async Task<KeyValuePair<string, int>> DoEmpregosFinalizados(int ano)
    {
        return await Task.Run(() => {
            var vagas = 0;

            foreach(var item in _datalist)
            {
                if(item.Status == "Finalizado")
                    vagas += item.Vagas;               
            }

            return new KeyValuePair<string, int>("Completadas", vagas);
          });
    }

    public async Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByGenero(int ano)
    {
        return await Task.Run(() => {
            int vagas = 0;
            int vagas_f = 0;
            int vagas_m = 0;

            var _return = new List<KeyValuePair<string, int>>();

            foreach(var item in _datalist)
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
        return await Task.Run(() => {
            int vagas = 0;
            int vagas_f = 0;
            int vagas_m = 0;

            var _return = new List<KeyValuePair<string, int>>();

            foreach(var item in _datalist)
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
 
    public async Task<IEnumerable<KeyValuePair<string, int>>> DoListOcupacoes(int ano)
    {
        return await Task.Run(() => {
            var _return = new List<KeyValuePair<string, int>>();
            var _list = new List<string>();

            foreach(var item in _datalist) {  
                for(int i = 0; i < item.Vagas; i++) {
                    _list.Add(item.Ocupacao);
                }         
            }

            foreach(var x in from a in _list
                group a by a into g
                let count = g.Count()
                orderby count descending
                select new { Servico = g.Key, Qtde = count })
            {
                _return.Add(new KeyValuePair<string, int> (x.Servico, x.Qtde));
            }

            return _return;
        });
    }

    public async Task<IEnumerable<(string month, int valor, string percent)>> DoListVagasByMonth(int ano)
    {

        return await Task.Run(() => {

            var _return = new List<(string month, int valor, string percent)>();
            var _month = new List<string>();

            foreach (var item in _datalist) {
                for(int i = 0; i < item.Vagas; i++) {                    
                    _month.Add(item.Data.Value.Month.ToString("MMM"));                    
                }                
            }

            foreach(var x in from a in _month
                group a by a into g
                let count = g.Count()
                orderby count descending
                select new { Servico = g.Key, Qtde = count })
            {
                

                _return.Add((x.Servico, x.Qtde, "" ));
            }

            return _return;
        });
    }

    public Task<IEnumerable<(string setor, int valor, string percent)>> DoListVagasBySetor(int ano)
    {
        throw new NotImplementedException();
    }
}