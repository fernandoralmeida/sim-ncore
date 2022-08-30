using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service;

public class ServiceBIAtendimento : IServiceBIAtendimento {
    
    private readonly IRepositoryAtendimento _atendimento;
    public ServiceBIAtendimento(IRepositoryAtendimento repositoryAtendimento) {
        _atendimento = repositoryAtendimento;        
    }
    
    public async Task<EChartDual> DoAsync(int ano)
    {
        return await Task.Run(async () => {

            var src = 0;
            var _at = await _atendimento.DoListByAnoAsync(ano);

            foreach(var item in _at.Where(s => s.Servicos != null))
            { 
                string[] servicos = item.Servicos.ToString().Split(new char[] { ';', ',' });                    
                foreach(var s in servicos) {
                    src ++;
                }                
            }
            return new EChartDual("Atendimentos", _at.Where(s => s.Servicos != null).Count(), src);
        });
    }

    public async Task<IEnumerable<EChartDual>> DoListCanalAsync(int ano)
    {
        return await Task.Run(async () => {
            var _list = new List<EChartDual>();
            var _count_at = new List<string>();
            var _count_sv = new List<string>();
            var _at = await _atendimento.DoListByAnoAsync(ano);

            foreach(var item in _at.Where(s => s.Servicos != null && s.Canal != null)) {
                foreach(var s in item.Servicos.Split(new char[] {';', ','})) {
                    _count_sv.Add(item.Canal);
                } 
                _count_at.Add(item.Canal);  
            }

            foreach(var x in from a in _count_at
                                group a by a into g
                                let count = g.Count()
                                orderby count descending
                                select new { Name = g.Key, Value = count })
            {
                _list.Add(new EChartDual(x.Name, x.Value, _count_sv.Where(s => s.Contains(x.Name)).Count()));
            }
            
            return _list;
        });
    }

    public async Task<IEnumerable<EChart>> DoListCanalPercentAsync(int ano)
    {
        return await Task.Run(async () => {
            var _list = new List<EChart>();
            var _at = await _atendimento.DoListByAnoAsync(ano);

            foreach(var item in _at.Where(s => s.Servicos != null)
                                    .GroupBy(s => s.Canal)
                                    .OrderByDescending(s => s.Count())) {
                _list.Add(new EChart(item.Key, item.Count(), string.Empty));              
            }

            return _list;
        });
    }

    public async Task<IEnumerable<EChart>> DoListClientesAsync(int ano)
    {
        return await Task.Run(async () => {

            var _list = new List<EChart>();

            var _at = await _atendimento.DoListByAnoAsync(ano);
            var _an = _at.Where(s => s.Anonimo == true).Count();            
            var _pj = _at.Where(s => s.Empresa != null).Count() - _an;
            var _pf = _at.Where(s => s.Pessoa != null).Count() - _pj;          

            _list.Add(new EChart("Pessoas", _pf, string.Empty));
            _list.Add(new EChart("Empresas", _pj, string.Empty));
            _list.Add(new EChart("Anônimo", _an, string.Empty));

            return _list;
        });
    }

    public Task<IEnumerable<EChartDual>> DoListMonthAsync(int ano)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<EChart>> DoListServiceAsync(int ano)
    {
        return await Task.Run(async () => {

            var _servicos = new List<string>();
            var _total = new List<EChart>();

            var _at = await _atendimento.DoListByAnoAsync(ano);

            foreach(var item in _at.Where(s => s.Servicos != null))
            { 
                string[] servicos = item.Servicos.ToString().Split(new char[] { ';', ',' });                    
                foreach(var s in servicos) {
                    _servicos.Add(s);
                }                
            }

            foreach(var x in from a in _servicos
                                group a by a into g
                                let count = g.Count()
                                orderby count descending
                                select new { Servico = g.Key, Qtde = count })
            {
                _total.Add(new EChart(x.Servico, x.Qtde, string.Empty));
            }

            return _total;
        });
    }

    public async Task<IEnumerable<EChartDual>> DoListSetorAsync(int ano)
    {
        return await Task.Run(async () => {
            var _list = new List<EChartDual>();
            var _count_at = new List<string>();
            var _count_sv = new List<string>();
            var _at = await _atendimento.DoListByAnoAsync(ano);

            foreach(var item in _at.Where(s => s.Servicos != null)) {
                foreach(var s in item.Servicos.Split(new char[] {';', ','})) {
                    _count_sv.Add(item.Setor);
                } 
                _count_at.Add(item.Setor);  
            }

            foreach(var x in from a in _count_at
                                group a by a into g
                                let count = g.Count()
                                orderby count descending
                                select new { Name = g.Key, Value = count })
            {
                _list.Add(new EChartDual(x.Name, x.Value, _count_sv.Where(s => s.Contains(x.Name)).Count()));
            }
            
            return _list;
        });
    }

    public async Task<IEnumerable<EChart>> DoListSetorPercentAsync(int ano)
    {
        return await Task.Run(async () => {
            var _list = new List<EChart>();
            var _at = await _atendimento.DoListByAnoAsync(ano);

            foreach(var item in _at.Where(s => s.Servicos != null)
                                    .GroupBy(s => s.Setor)
                                    .OrderByDescending(s => s.Count())) {
                _list.Add(new EChart(item.Key, item.Count(), string.Empty));              
            }

            return _list;
        });
    }

    public async Task<IEnumerable<EChartDual>> DoListUserAsync(int ano)
    {
        return await Task.Run(async () => {
            var _list = new List<EChartDual>();
            var _count_at = new List<string>();
            var _count_sv = new List<string>();
            var _at = await _atendimento.DoListByAnoAsync(ano);

            foreach(var item in _at.Where(s => s.Servicos != null)) {                        
                foreach(var s in item.Servicos.Split(new char[] {';', ','})) {
                    _count_sv.Add(item.Owner_AppUser_Id);
                } 
                _count_at.Add(item.Owner_AppUser_Id);              
            }

            foreach(var x in from a in _count_at
                                group a by a into g
                                let count = g.Count()
                                orderby count descending
                                select new { User = g.Key, Atend = count })
            {
                _list.Add(new EChartDual(x.User, x.Atend, _count_sv.Where(s => s.Contains(x.User)).Count()));
            }

            return _list;
        });
    }
}