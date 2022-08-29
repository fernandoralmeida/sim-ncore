using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Validations;

namespace Sim.Domain.Service;
public class ServiceBIEmpregos : IServiceBIEmpregos
{
    private readonly IRepositoryEmpregos _repositoryEmpregos; 

    public ServiceBIEmpregos(IRepositoryEmpregos repositoryEmpregos) {
        _repositoryEmpregos = repositoryEmpregos;
    }
    public async Task<EChart> DoEmpregosAtivos(int ano)
    {
        return await Task.Run(async () => {
            var vagas = 0;
            var t_vagas = 0;

            foreach(var item in await _repositoryEmpregos.DoListEmpregosAsyncByAno(ano))
            {
                t_vagas += item.Vagas;
                if(item.Status == "Ativo")
                    vagas += item.Vagas;               
            }
            float r = ((float)vagas / (float)t_vagas) * 100;
            return new EChart("Disponível", vagas, r.ToString("N2")+"%");
          });
    }    

    public async Task<EChart> DoEmpregosAtivosAcumulado(int ano)
    {
        return await Task.Run(async () => {
            var vagas = 0;

            foreach(var item in await _repositoryEmpregos.DoListEmpregosAsyncByAno(ano))
            {
                vagas += item.Vagas;               
            }

            float r = ((float)vagas / (float)vagas) * 100;

            return new EChart("Acumulado", vagas, r.ToString("N2")+"%");
          });
    }

    public async Task<EChart> DoEmpregosFinalizados(int ano)
    {
        return await Task.Run(async () => {
            var vagas = 0;
            var t_vagas = 0;

            foreach(var item in await _repositoryEmpregos.DoListEmpregosAsyncByAno(ano))
            {
                t_vagas += item.Vagas;
                if(item.Status == "Finalizado")
                    vagas += item.Vagas;               
            }

            float r = ((float)vagas / (float)t_vagas) * 100;

            return new EChart("Completadas", vagas, r.ToString("N2")+"%");
          });
    }

    public async Task<IEnumerable<EChart>> DoListEmpregosAtivosByGenero(int ano)
    {
        return await Task.Run(async () => {
            int t_vagas = 0;
            float vn = 0.0f;
            float vm = 0.0f;
            float vf = 0.0f;    
            
            int vagas = 0;
            int vagas_f = 0;
            int vagas_m = 0;

            var _return = new List<EChart>();

            foreach(var item in await _repositoryEmpregos.DoListEmpregosAsyncByAno(ano))
            {
                t_vagas += item.Vagas;
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
            vn = ((float)vagas / (float)t_vagas) * 100;
            vm = ((float)vagas_m / (float)t_vagas) * 100;
            vf = ((float)vagas_f / (float)t_vagas) * 100;
            _return.Add(new EChart("Neutro", vagas, vn.ToString("N2")+"%"));
            _return.Add(new EChart("Masculino", vagas_m, vm.ToString("N2")+"%"));
            _return.Add(new EChart("Feminino", vagas_f, vf.ToString("N2")+"%"));

            return _return.OrderBy(o => o.Value);
          });
    }

    public async Task<IEnumerable<EChart>> DoListEmpregosAtivosByGeneroAcumulado(int ano)
    {
        return await Task.Run(async () => {
            float vn = 0.0f;
            float vm = 0.0f;
            float vf = 0.0f;

            int vagas = 0;
            int vagas_f = 0;
            int vagas_m = 0;

            int t_vagas = 0;
            var _return = new List<EChart>();

            foreach(var item in await _repositoryEmpregos.DoListEmpregosAsyncByAno(ano))
            {
                t_vagas += item.Vagas;
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

            vn = ((float)vagas / (float)t_vagas) * 100;
            vm = ((float)vagas_m / (float)t_vagas) * 100;
            vf = ((float)vagas_f / (float)t_vagas) * 100;

            _return.Add(new EChart("Neutro", vagas, vn.ToString("N2")+"%"));
            _return.Add(new EChart("Masculino", vagas_m, vm.ToString("N2")+"%"));
            _return.Add(new EChart("Feminino", vagas_f, vf.ToString("N2")+"%"));

            return _return.OrderBy(o => o.Value);
          });
    }

    public async Task<IEnumerable<EChart>> DoListEmpregosAtivosByInclusao(int ano)
    {
        return await Task.Run(() => {
            var _return = new List<EChart>();


            return _return;
          });
    }

    public async Task<IEnumerable<EChart>> DoListEmpregosAtivosByInclusaoAcumulado(int ano)
    {
        return await Task.Run(() => {
            var _return = new List<EChart>();


            return _return;
          });
    }

    public async Task<IEnumerable<EChart>> DoListEmpregosAtivosByTipo(int ano)
    {
        return await Task.Run(() => {
            var _return = new List<EChart>();


            return _return;
          });
    }

    public async Task<IEnumerable<EChart>> DoListEmpregosAtivosByTipoAcumulado(int ano)
    {
        return await Task.Run(() => {
            var _return = new List<EChart>();


            return _return;
          });
    }
 
    public async Task<IEnumerable<EChart>> DoListOcupacoes(int ano)
    {
        return await Task.Run(async () => {
            var _return = new List<EChart>();
            var _list = new List<string>();
            var t_vagas = 0;

            foreach(var item in await _repositoryEmpregos.DoListEmpregosAsyncByAno(ano)) {  
                t_vagas += item.Vagas;
                for(int i = 0; i < item.Vagas; i++) {
                    if(item.Ocupacao != null)
                        _list.Add(item.Ocupacao.NormalizeText().ToUpper());
                }         
            }

            foreach(var x in from a in _list
                group a by a into g
                let count = g.Count()
                orderby count descending
                select new { Servico = g.Key, Qtde = count })
            {
                float v1 = x.Qtde;
                float v2 = t_vagas;
                float r = (v1 / v2) * 100;
                _return.Add(new EChart(x.Servico, x.Qtde, r.ToString("N2")+"%"));
            }

            return _return;
        });
    }

    public async Task<IEnumerable<EChart>> DoListVagasByMonth(int ano)
    {
        return await Task.Run(async () => {

            var _return = new List<EChart>();
            var _month = new List<string>();
            var t_vagas = 0;

            foreach (var item in await _repositoryEmpregos.DoListEmpregosAsyncByAno(ano)) {
                t_vagas += item.Vagas;
                for(int i = 0; i < item.Vagas; i++) {     
                    _month.Add(item.Data.Value.Date.ToString("MMM"));                    
                }                
            }

            foreach(var x in from a in _month
                group a by a into g
                let count = g.Count()
                //orderby count descending
                select new { Meses = g.Key, Valor = count })
            {

                float p = x.Valor;
                float p1 = t_vagas;
                float r = (p / p1) * 100;          
                _return.Add(new EChart(x.Meses, x.Valor, r.ToString("N2")+"%"));
            }

            return _return;
        });
    }

    public async Task<IEnumerable<EChart>> DoListVagasBySetor(int ano)
    {
        return await Task.Run(async () => {

            var _return = new List<EChart>();
            var _setor = new List<string>();
            var t_vagas = 0;

            foreach (var item in await _repositoryEmpregos.DoListEmpregosAsyncByAno(ano)) {
                t_vagas += item.Vagas;
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
                        for (int i = 0; i < item.Vagas; i++) {
                            _setor.Add("CPF");
                        }  
                }                               
            }

            foreach(var x in from a in _setor
                group a by a into g
                let count = g.Count()
                orderby count descending
                select new { Setores = g.Key, Valor = count })
            {

                float p = x.Valor;
                float p1 = t_vagas;
                float r = (p / p1) * 100;          
                _return.Add(new EChart(x.Setores, x.Valor, r.ToString("N2")+"%"));
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