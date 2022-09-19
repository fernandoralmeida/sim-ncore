namespace Sim.Domain.Cnpj.Entity;

public class EExport {

    public EExport(
            int contador, string cnpj, string razaosocial,
            string matriz, string abertura, string situacao,
            string zona, string logradouro, string numero, string localizacao, string municipio,
            string cnae, string atividade, string porte,
            string regimefiscal, string setor){
        Contador = contador;
        Cnpj = cnpj.Trim();
        RazaoSocial = razaosocial.Trim();
        Matriz = matriz.Trim();
        Abertura = abertura.Trim();
        Situacao = situacao.Trim();
        Zona = zona.Trim();        
        Logradouro = logradouro.Trim();
        Numero = numero.Trim();
        Localizacao = localizacao.Trim();
        Municipio = municipio.Trim();
        Cnae = cnae.Trim();
        AtividadePrincipal = atividade.Trim();
        Porte = porte.Trim();
        RegimeFiscal = regimefiscal.Trim();
        Setor = setor.Trim();
    }
    public int Contador { get; private set; }
    public string Cnpj { get; private set; }
    public string RazaoSocial { get; private set; }
    public string Matriz { get; private set; }
    public string Abertura { get; private set; }
    public string Situacao { get; private set; }
    public string Zona { get; private set; }
    public string Logradouro { get; private set; }
    public string Numero { get; private set;}
    public string Localizacao { get; private set; }
    public string Municipio { get; private set; }
    public string Cnae { get; private set; }
    public string AtividadePrincipal { get; private set; }
    public string Porte { get; private set; }
    public string RegimeFiscal { get; private set; }
    public string Setor { get; private set; }
}