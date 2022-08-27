using Sim.Domain.Entity;

namespace Sim.Domain.Interface.IService;
public interface IServiceBIEmpregos {
    Task<EChart> DoEmpregosAtivos(int ano);
    Task<EChart> DoEmpregosFinalizados(int ano);
    Task<EChart> DoEmpregosAtivosAcumulado(int ano);
    Task<IEnumerable<EChart>> DoListEmpregosAtivosByGenero(int ano);
    Task<IEnumerable<EChart>> DoListEmpregosAtivosByGeneroAcumulado(int ano);
    Task<IEnumerable<EChart>> DoListEmpregosAtivosByTipo(int ano);
    Task<IEnumerable<EChart>> DoListEmpregosAtivosByTipoAcumulado(int ano);
    Task<IEnumerable<EChart>> DoListEmpregosAtivosByInclusao(int ano);
    Task<IEnumerable<EChart>> DoListEmpregosAtivosByInclusaoAcumulado(int ano);
    Task<IEnumerable<EChart>> DoListOcupacoes(int ano);
    Task<IEnumerable<EChart>> DoListVagasBySetor(int ano);
    Task<IEnumerable<EChart>> DoListVagasByMonth(int ano);
}