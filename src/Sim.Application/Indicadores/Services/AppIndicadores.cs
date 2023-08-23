using System.Linq.Expressions;
using Sim.Application.Indicadores.Interfaces;
using Sim.Application.Indicadores.VModel;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using Sim.Domain.Helpers;

namespace Sim.Application.Indicadores.Services;

public class AppIndicadores : IAppIndicadores
{
    public readonly IAppServiceAtendimento? _appatendimento;

    public AppIndicadores(IAppServiceAtendimento appatendimento)
    {
        _appatendimento = appatendimento;
    }
    public async Task<VmRAtendimentos> DoAtendimentosAsync(Expression<Func<EAtendimento, bool>>? filter = null)
    {
        var atendimentos = await _appatendimento!.DoListAsync(filter!);
        return await Task.Run(() =>
         {

             var _report = new VmRAtendimentos();
             float _at_count = atendimentos.Count();

             _report.Atendimentos = new KeyValuePair<string, int>("Atendimentos", atendimentos.Count());

             var _atmonth = new List<KeyValuePair<string, int>>();
             foreach (var item in atendimentos.Where(s => s.Servicos != null)
                                     .OrderBy(o => o.Data)
                                     .GroupBy(g => g.Data!.Value.ToString("MMM")))
             {
                 _atmonth.Add(new KeyValuePair<string, int>(item.Key, item.Count()));
             }
             _report.AtendimentosMonth = _atmonth;

             var _svmonth = new List<string>();
             var _getservices = new List<string>();
             var _serv_time_day = new List<string>();
             foreach (var item in atendimentos.Where(s => s.Servicos != null).OrderBy(o => o.Data))
             {
                 foreach (var s in item.Servicos!.Split(new char[] { ';', ',' }))
                 {
                     _getservices.Add(s);
                     _svmonth.Add(item.Data!.Value.Date.ToString("MMM"));
                     _serv_time_day.Add(item.Data.Value.ToString("HH"));
                 }
             }
             _report.Serviços = new KeyValuePair<string, int>("Serviços", _svmonth.Count());

             var _ls_svmonth = new List<KeyValuePair<string, int>>();
             foreach (var item in _svmonth.GroupBy(g => g))
             {
                 _ls_svmonth.Add(new KeyValuePair<string, int>(item.Key, item.Count()));
             }
             _report.ServicesMonth = _ls_svmonth;

             var _list_svc = new List<KeyValuePair<string, int>>();
             foreach (var item in _getservices.GroupBy(g => g)
                                                 .OrderByDescending(o => o.Count()))
             {
                 _list_svc.Add(new KeyValuePair<string, int>(item.Key, item.Count()));
             }
             _report.ListaServicos = _list_svc;

             _report.Canais = from cn in atendimentos
                                        .Where(s => s.Canal != null)
                                        .GroupBy(g => g.Canal)
                                        .OrderByDescending(o => o.Count())
                              select (cn.Key, cn.Count(), cn.Count() / _at_count * 100F);

             var _c = new List<KeyValuePair<string, int>>();
             _c.Add(new KeyValuePair<string, int>("Pessoas", atendimentos.Where(s => s.Pessoa != null && s.Empresa == null).Count()));
             _c.Add(new KeyValuePair<string, int>("Empresas", atendimentos.Where(s => s.Empresa != null).Count()));
             _c.Add(new KeyValuePair<string, int>("Anônimo", atendimentos.Where(s => s.Pessoa == null && s.Empresa == null).Count()));
             _report.PerfilAtendimento = _c;

             var _pc = new List<KeyValuePair<string, int>>();
             _pc.Add(new KeyValuePair<string, int>("Pessoas", atendimentos.Where(s => s.Pessoa != null && s.Empresa == null).DistinctBy(s => s.Pessoa).Count()));
             _pc.Add(new KeyValuePair<string, int>("Empresas", atendimentos.Where(s => s.Empresa != null).DistinctBy(s => s.Empresa).Count()));
             _report.PerfilCliente = _pc;

             var _faixa_etaria = new List<string>();
             var _list_faixa = new List<KeyValuePair<string, int>>();
             var _list_genero = new List<KeyValuePair<string, int>>();

             foreach (var i in atendimentos.Where(s => s.Pessoa != null).DistinctBy(s => s.Pessoa))
             {

                 var d1 = new DateTime(i.Pessoa!.Data_Nascimento!.Value.Year,
                                         i.Pessoa.Data_Nascimento.Value.Month,
                                         i.Pessoa.Data_Nascimento.Value.Day);

                 var d2 = new DateTime(i.Data!.Value.Year,
                                         i.Data.Value.Month,
                                         i.Data.Value.Day);

                 var _faixa = (d2.Subtract(d1).TotalDays) / 365;
                 if (_faixa < 16)
                     _faixa_etaria.Add("Erro: - 16");
                 else if (_faixa > 15 && _faixa < 21)
                     _faixa_etaria.Add("16 - 20");
                 else if (_faixa > 20 && _faixa < 31)
                     _faixa_etaria.Add("21 - 30");
                 else if (_faixa > 30 && _faixa < 41)
                     _faixa_etaria.Add("31 - 40");
                 else if (_faixa > 40 && _faixa < 51)
                     _faixa_etaria.Add("41 - 50");
                 else if (_faixa > 50 && _faixa < 61)
                     _faixa_etaria.Add("51 - 60");
                 else if (_faixa > 60 && _faixa < 71)
                     _faixa_etaria.Add("61 - 70");
                 else if (_faixa > 70)
                     _faixa_etaria.Add("71 ou mais");
             }

             foreach (var item in _faixa_etaria.GroupBy(g => g)
                                                 .OrderByDescending(o => o.Count()))
             {
                 _list_faixa.Add(new KeyValuePair<string, int>(item.Key, item.Count()));
             }
             _report.FaixaEtariaCliente = _list_faixa;

             foreach (var item in atendimentos.Where(s => s.Pessoa != null)
                                     .DistinctBy(s => s.Pessoa)
                                     .GroupBy(g => g.Pessoa!.Genero)
                                     .OrderByDescending(o => o.Count()))
             {
                 _list_genero.Add(new KeyValuePair<string, int>(item.Key!, item.Count()));
             }

             _report.GeneroCliente = _list_genero;

             var _cli = atendimentos.Where(s => s.Pessoa != null)
                             .Where(s => s.Empresa == null)
                             .DistinctBy(s => s.Pessoa)
                             .ToList();

             var _novos_recorrentes = new List<KeyValuePair<string, int>>();

             _novos_recorrentes.Add(new KeyValuePair<string, int>("Novos", _cli
                                     .Where(s => s.Pessoa!.Data_Cadastro!.Value.Year == s.Data!.Value.Year)
                                     .Count()));

             _novos_recorrentes.Add(new KeyValuePair<string, int>("Recorrentes", _cli
                                     .Where(s => s.Pessoa!.Data_Cadastro!.Value.Year < s.Data!.Value.Year)
                                     .Count()));

             _report.Clientes = _novos_recorrentes;

             var _emp = atendimentos.Where(s => s.Empresa != null)
                             .DistinctBy(s => s.Empresa)
                             .ToList();

             var _emp_novos_recorrentes = new List<KeyValuePair<string, int>>();

             _emp_novos_recorrentes.Add(new KeyValuePair<string, int>(
                 "Novos", _emp
                             .Where(s => s.Empresa!.Data_Abertura!.Value.Year == s.Data!.Value.Year)
                             .Count()
             ));

             _emp_novos_recorrentes.Add(new KeyValuePair<string, int>(
                 "Recorrentes", _emp
                                 .Where(s => s.Empresa!.Data_Abertura!.Value.Year < s.Data!.Value.Year)
                                 .Count()
             ));

             _report.Empresas = _emp_novos_recorrentes;

             // - Empresas
             var _emp_idade = new List<string>();
             var _list_emp_idade = new List<(string, int, float)>();
             var _setores = new List<string>();
             var _list_Setores = new List<(string, int, float)>();
             var _location = new List<string>();
             var _list_location = new List<(string, int, float)>();

             var _e_cont = 0.0F;

             foreach (var i in atendimentos.Where(s => s.Empresa != null).DistinctBy(s => s.Empresa))
             {

                 var d1 = new DateTime(i.Empresa!.Data_Abertura!.Value.Year,
                                         i.Empresa.Data_Abertura.Value.Month,
                                         i.Empresa.Data_Abertura.Value.Day);

                 var d2 = new DateTime(i.Data!.Value.Year,
                                         i.Data.Value.Month,
                                         i.Data.Value.Day);

                 var _faixa = (d2.Subtract(d1).TotalDays) / 365;

                 if (_faixa <= 2)
                     _emp_idade.Add("até 2");
                 else if (_faixa > 2 && _faixa <= 5)
                     _emp_idade.Add("2 a 5");
                 else if (_faixa > 5 && _faixa <= 10)
                     _emp_idade.Add("5 a 10");
                 else if (_faixa > 10)
                     _emp_idade.Add("10 ou mais");

                 string _cnae = i.Empresa.CNAE_Principal!.Remove(2, 8);
                 if (_cnae.All(char.IsDigit))
                     _setores.Add(Convert.ToInt32(_cnae).DoSetores());

                 _location.Add(i.Empresa.Bairro!);

                 _e_cont++;
             }

             foreach (var item in _emp_idade
                                     .GroupBy(g => g)
                                     .OrderByDescending(o => o.Count()))
             {
                 _list_emp_idade.Add((item.Key, item.Count(), (item.Count() / _e_cont) * 100F));
             }
             _report.EmpresasIdade = _list_emp_idade;

             foreach (var item in _setores
                                     .Where(s => s != "...")
                                     .GroupBy(g => g)
                                     .OrderByDescending(o => o.Count()))
                 _list_Setores.Add((item.Key, item.Count(), (item.Count() / _e_cont) * 100F));

             _report.EmpresasSetores = _list_Setores;

             foreach (var item in _location.GroupBy(g => g).OrderByDescending(o => o.Count()))
                 _list_location.Add((item.Key, item.Count(), (item.Count() / _e_cont) * 100F));

             _report.TimeDay = from td in atendimentos
                                        .Where(s => s.Servicos != null)
                                        .GroupBy(d => d.Data!.Value.ToString("HH"))
                                        .OrderBy(o => o.Key)
                               select (td.Key, td.Count());

             _report.ServTimeDay = from svt in _serv_time_day
                                        .GroupBy(g => g)
                                        .OrderBy(o => o.Key)
                                   select (svt.Key, svt.Count());

             _report.EmpresasLocation = _list_location;

             return _report;
         });
    }
}