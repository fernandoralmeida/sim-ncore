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

            foreach (var item in _datalist.Where(o => o.Data != null)) {
                for(int i = 0; i < item.Vagas; i++) {     
                    _month.Add(item.Data.Value.Date.ToString("MMM"));                    
                }                
            }

            foreach(var x in from a in _month
                group a by a into g
                let count = g.Count()
                orderby count descending
                select new { Meses = g.Key, Valor = count })
            {

                float p = x.Valor;
                float p1 = _month.Count();
                float r = (p / p1) * 100;          
                _return.Add((x.Meses, x.Valor, r.ToString("N2")+"%"));
            }

            return _return;
        });
    }

    public async Task<IEnumerable<(string setor, int valor, string percent)>> DoListVagasBySetor(int ano)
    {
        return await Task.Run(() => {

            var _return = new List<(string setor, int valor, string percent)>();
            var _setor = new List<string>();

            foreach (var item in _datalist) {
                if (item.Empresa != null) {
                    if (item.Empresa.CNAE_Principal != null) {
                        string _cnae = item.Empresa.CNAE_Principal.Remove(2,8);
                        if (_cnae.All(char.IsDigit)) {
                            for (int i = 0; i < item.Vagas; i++) {
                                _setor.Add(DoSetores(Convert.ToInt32(_cnae)));
                            }                            
                        }                        
                    }
                }
                else {
                    if(item.Pessoa != null)
                        _setor.Add("CPF");
                }                               
            }

            foreach(var x in from a in _setor
                group a by a into g
                let count = g.Count()
                orderby count descending
                select new { Setores = g.Key, Valor = count })
            {

                float p = x.Valor;
                float p1 = _setor.Count();
                float r = (p / p1) * 100;          
                _return.Add((x.Setores, x.Valor, r.ToString("N2")+"%"));
            }

            return _return;
        });
    }

    private string DoSetores(int cnae) {               
        if (cnae >= 1 && cnae <= 3) {
            return "Agropecuária";
        }
        else if (cnae >= 45 && cnae <= 47) {
           return "Comércio";
        }
        else if (cnae >= 05 & cnae <= 09 || cnae >= 10 && cnae <= 33) {
           return "Indústria";
        }
        else if (cnae >= 41 & cnae <= 43) {
           return "Construção";
        }
        else if (cnae == 35 || (cnae >= 36 && cnae <= 39)
            || (cnae >= 49 && cnae <= 53)
            || (cnae >= 55 && cnae <= 56)
            || (cnae >= 58 && cnae <= 63)
            || (cnae >= 64 && cnae <= 66)
            || (cnae == 68)
            || (cnae >= 69 && cnae <= 75)
            || (cnae >= 77 && cnae <= 82)
            || (cnae == 85)
            || (cnae >= 86 && cnae <= 88)
            || (cnae >= 86 && cnae <= 88)
            || (cnae >= 90 && cnae <= 93)
            || (cnae >= 94 && cnae <= 96)
            || (cnae == 97)
            || (cnae == 99)) {
            return "Serviços";
        }
        else {
            return "PF";
        }
    }
}