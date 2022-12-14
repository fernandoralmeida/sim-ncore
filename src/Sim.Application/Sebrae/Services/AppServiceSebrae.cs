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
            _meis.Add(new KeyValuePair<string, int>("Formalização-MEI", at.Where(s => s.Servicos.Contains("Alteração-MEI")).Count()));
            _meis.Add(new KeyValuePair<string, int>("Formalização-MEI", at.Where(s => s.Servicos.Contains("Baixa-MEI")).Count()));
            _report.MEIs = _meis;

            var _c = new List<KeyValuePair<string, int>>();
            _c.Add(new KeyValuePair<string, int>("Pessoa Física",at.Where(s => s.Pessoa != null && s.Empresa == null).Count()));
            _c.Add(new KeyValuePair<string, int>("Pessoa Jurídica",at.Where(s => s.Empresa != null).Count()));
            _c.Add(new KeyValuePair<string, int>("Anônimo",at.Where(s => s.Pessoa == null && s.Empresa == null).Count()));
            _report.PerfilCliente = _c;

            
            return _report;
        });
    }
}