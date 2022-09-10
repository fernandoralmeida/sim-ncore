namespace Sim.Domain.Cnpj.Entity;
public class EMapping {

    public EMapping(string bairro, int nempresas, IEnumerable<KeyValuePair<string, int>> ruas) {
        Bairro = bairro;
        NEmpresas = nempresas;
        Ruas = ruas;
    }
    public string Bairro { get; private set;}

    public int NEmpresas { get; private set; }
    // Retorna lista de ruas e numero de empresas
    public IEnumerable<KeyValuePair<string, int>> Ruas { get; private set;  }
}