namespace Sim.Domain.Sebrae.Model;
public class EReports {
    public KeyValuePair<string, int> Atendimentos { get; set; }
    public KeyValuePair<string, int> Serviços { get; set; }
    public KeyValuePair<string, int> Eventos { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? AtendimentosMonth { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? ServicesMonth { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? EventosMonth { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? ListaServicos { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? PerfilCliente { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? FaixaEtariaCliente { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? GeneroCliente { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? Clientes { get; set; }
 }