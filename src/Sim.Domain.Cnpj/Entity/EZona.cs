namespace Sim.Domain.Cnpj.Entity;

public class EMapping {
    public EMapping(string zona, IEnumerable<ELocalizacao> localizacoes, IEnumerable<KeyValuePair<string, int>> localizacoesagrupadas ) {   
        Zona = zona;
        Localizacoes = localizacoes;
        LocalizacoesAgrupadas = localizacoesagrupadas;
    }
    public string Zona { get; private set; }
    public IEnumerable<ELocalizacao> Localizacoes { get; private set; }
    public IEnumerable<KeyValuePair<string, int>> LocalizacoesAgrupadas {get; private set;}
}