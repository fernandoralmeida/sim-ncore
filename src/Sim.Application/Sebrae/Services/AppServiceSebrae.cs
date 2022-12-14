using Sim.Application.Sebrae.Interfaces;
using Sim.Domain.Entity;
using Sim.Domain.Evento.Model;
using Sim.Domain.Sebrae.Model;

namespace Sim.Application.Sebrae.Services;

public class AppServiceSebrae : IAppServiceSebrae
{
    public async Task<EReports> DoReportAsync(IEnumerable<EAtendimento> at, IEnumerable<EEvento> ev)
    {
        return await Task.Run(() => {

            var _report = new EReports();

            _report.Atendimentos = new KeyValuePair<string, int>("Atendimentos", at.Count());
            _report.Eventos = new KeyValuePair<string, int>("Eventos", ev.Count());

            var _atmonth = new List<KeyValuePair<string, int>>();
            foreach (var item in at.Where(s => s.Servicos != null)
                                    .OrderBy(o => o.Data)
                                    .GroupBy(g => g.Data.Value.ToString("MMM"))) {
                _atmonth.Add(new KeyValuePair<string, int>(item.Key, item.Count()));                
            }            
            
            var _svmonth = new List<string>();
            foreach (var item in at.Where(s => s.Servicos != null).OrderBy(o => o.Data)) {                
                foreach(var s in item.Servicos.Split(new char[] {';', ','})) {
                    _svmonth.Add(item.Data.Value.Date.ToString("MMM"));
                }               
            }
            _report.Serviços = new KeyValuePair<string, int>("Serviços", _svmonth.Count());
            var _ls_svmonth = new List<KeyValuePair<string, int>>();
            foreach (var item in _svmonth.GroupBy(g => g)) {
                _ls_svmonth.Add(new KeyValuePair<string, int>(item.Key, item.Count()));                
            }
            _report.ServicesMonth = _ls_svmonth;

            var _meis = new List<KeyValuePair<string, int>>();
            _meis.Add(new KeyValuePair<string, int>("Formalização-MEI", at.Where(s => s.Servicos.Contains("Formalização-MEI")).Count()));
            _meis.Add(new KeyValuePair<string, int>("Alteração-MEI", at.Where(s => s.Servicos.Contains("Alteração-MEI")).Count()));
            _meis.Add(new KeyValuePair<string, int>("Baixa-MEI", at.Where(s => s.Servicos.Contains("Baixa-MEI")).Count()));
            _report.MEIs = _meis;

            var _c = new List<KeyValuePair<string, int>>();
            _c.Add(new KeyValuePair<string, int>("Pessoa Física",at.Where(s => s.Pessoa != null && s.Empresa == null).Count()));
            _c.Add(new KeyValuePair<string, int>("Pessoa Jurídica",at.Where(s => s.Empresa != null).Count()));
            _c.Add(new KeyValuePair<string, int>("Anônimo",at.Where(s => s.Pessoa == null && s.Empresa == null).Count()));
            _report.PerfilCliente = _c;

            var _faixa_etaria = new List<string>();
             var _list_faixa = new List<KeyValuePair<string, int>>();
            var _genero = new List<string>();
            var _list_genero = new List<KeyValuePair<string, int>>();
            
            foreach (var i in at.Where(s => s.Pessoa != null)) {

                _genero.Add(i.Pessoa.Genero);
                
                var d1 = new DateTime(i.Pessoa.Data_Nascimento.Value.Year,
                                        i.Pessoa.Data_Nascimento.Value.Month,
                                        i.Pessoa.Data_Nascimento.Value.Day);

                var d2 = new DateTime(i.Data.Value.Year,
                                        i.Data.Value.Month,
                                        i.Data.Value.Day);

                var _faixa = (d2.Subtract(d1).TotalDays) / 365;
                if (_faixa > 15 && _faixa < 21)
                    _faixa_etaria.Add("16 -> 20 anos");
                else if (_faixa > 20 && _faixa < 31)
                        _faixa_etaria.Add("21 -> 30 anos");
                else if (_faixa > 30 && _faixa < 41)
                    _faixa_etaria.Add("31 -> 40 anos");
                else if (_faixa > 40 && _faixa < 51)
                    _faixa_etaria.Add("41 -> 50 anos");
                else if (_faixa > 50 && _faixa < 61)
                    _faixa_etaria.Add("51 -> 60 anos");
                else if (_faixa > 60 && _faixa < 71)
                    _faixa_etaria.Add("61 -> 70 anos");
                else if (_faixa > 70)
                    _faixa_etaria.Add("71 anos ou mais");
            }

            foreach (var item in _faixa_etaria.GroupBy(g => g)
                                                .OrderByDescending(o => o.Count())) {
                _list_faixa.Add(new KeyValuePair<string, int>(item.Key, item.Count()));
            }
            _report.FaixaEtariaCliente = _list_faixa;

            foreach (var item in _genero.GroupBy(g => g)
                                                .OrderByDescending(o => o.Count())) {
                _list_genero.Add(new KeyValuePair<string, int>(item.Key, item.Count()));
            }
            _report.GeneroCliente = _list_genero;

            _report.ClienteNovo = new KeyValuePair<string, int>("Clientes Novos", at
                                    .Where(s => s.Pessoa != null & s.Pessoa.Data_Cadastro.Value.Year == DateTime.Now.Year)
                                    .Count());
            
            return _report;
        });
    }
}