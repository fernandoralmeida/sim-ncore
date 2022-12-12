
namespace Sim.Domain.BancoPovo.Models;

public class EReports {
    public KeyValuePair<string, int> ContratosLiquidados { get; set; }
    public KeyValuePair<string, int> ContratosCancelados { get; set; }
    public KeyValuePair<string, int> ContratosReprovados { get; set; }
    public KeyValuePair<string, int> ContratosEmAnalise { get; set; }
    public KeyValuePair<string, int> ContratosAprovadosRegulares { get; set; }
    public KeyValuePair<string, int> ContratosAprovadosInadimplente { get; set; }
    public KeyValuePair<string, int> ContratosRenegociados { get; set; }
    public string? TaxaInadimplencia { get; set; }
    public string? ValorContratosRegulares { get; set; }
    public string? ValorContratosInadimplentes { get; set; }
    public string? ValorContratosAnalise { get; set; }
    public string? ValorContratosRenegociados { get; set; }
    public IEnumerable<EContrato>? ListaContratos { get; set; }
}