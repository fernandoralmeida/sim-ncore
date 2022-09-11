namespace Sim.Domain.Cnpj.Entity;

public class ELocalizacao {
    public ELocalizacao(string zona, string rua, string numero) {
        Zona = zona;
        Rua = rua;
        Numero = numero;
    }

    public string Zona { get; private set; }
    public string Rua { get; private set; }
    public string Numero { get; private set; }
}