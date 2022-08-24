using Sim.Domain.Entity;

namespace Sim.Application.Interfaces;
public interface IAppServiceBIEmpregos {
    Task<KeyValuePair<string, int>> DoEmpregosAtivos(int ano);
    Task<KeyValuePair<string, int>> DoEmpregosAtivosAcumulado(int ano);
    Task<KeyValuePair<string, int>> DoEmpregosFinalizados(int ano);
    Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByGenero(int ano);
    Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByGeneroAcumulado(int ano);
    Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByTipo(int ano);
    Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByTipoAcumulado(int ano);
    Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByInclusao(int ano);
    Task<IEnumerable<KeyValuePair<string, int>>> DoListEmpregosAtivosByInclusaoAcumulado(int ano);
    Task<IEnumerable<KeyValuePair<string, int>>> DoListOcupacoes(int ano);
    Task<IEnumerable<(string Setor, int Valor, string percent)>> DoListVagasBySetor(int ano);
    Task<IEnumerable<(string month, int valor, string percent)>> DoListVagasByMonth(int ano);
}