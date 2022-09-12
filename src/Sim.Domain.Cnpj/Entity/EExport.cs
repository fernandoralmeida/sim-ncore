namespace Sim.Domain.Cnpj.Entity;

public class EExport {

    public EExport(
            int contador, string cnpj, string razaosocial,
            string matriz, string abertura, string situacao,
            string zona, string logradouro, string localizacao, string municipio,
            string cnae, string atividade, string porte,
            string regimefiscal, string setor){
        Contador = contador;
        Cnpj = cnpj.TrimStart();
        RazaoSocial = razaosocial.TrimStart();
        Matriz = matriz.TrimStart();
        Abertura = abertura.TrimStart();
        Situacao = situacao.TrimStart();
        Zona = zona.TrimStart();        
        Logradouro = logradouro.TrimStart();
        Localizacao = localizacao.TrimStart();
        Municipio = municipio.TrimStart();
        Cnae = cnae.TrimStart();
        AtividadePrincipal = atividade.TrimStart();
        Porte = porte.TrimStart();
        RegimeFiscal = regimefiscal.TrimStart();
        Setor = setor.TrimStart();
    }
    public int Contador { get; private set; }
    public string Cnpj { get; private set; }
    public string RazaoSocial { get; private set; }
    public string Matriz { get; private set; }
    public string Abertura { get; private set; }
    public string Situacao { get; private set; }
    public string Zona { get; private set; }
    public string Logradouro { get; set; }
    public string Localizacao { get; private set; }
    public string Municipio { get; private set; }
    public string Cnae { get; private set; }
    public string AtividadePrincipal { get; private set; }
    public string Porte { get; private set; }
    public string RegimeFiscal { get; private set; }
    public string Setor { get; private set; }
}