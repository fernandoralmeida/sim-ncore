namespace Sim.Domain.Cnpj.Entity;

public class EExport {

    public EExport(
            int contador, string cnpj, string razaosocial,
            string matriz, string abertura, string situacao,
            string zona, string endereco, string municipio, string cnae, string atividade,
            string porte, string regimefiscal, string setor){
        Contador = contador;
        Cnpj = cnpj;
        RazaoSocial = razaosocial;
        Matriz = matriz;
        Abertura = abertura;
        Situacao = situacao;
        Zona = zona;        
        Endereco = endereco;
        Municipio = Municipio;
        Cnae = cnae;
        AtividadePrincipal = atividade;
        Porte = porte;
        RegimeFiscal = regimefiscal;
        Setor = setor;
    }
    public int Contador { get; private set; }
    public string Cnpj { get; private set; }
    public string RazaoSocial { get; private set; }
    public string Matriz { get; private set; }
    public string Abertura { get; private set; }
    public string Situacao { get; private set; }
    public string Zona { get; private set; }
    public string Endereco { get; private set; }
    public string Municipio { get; private set; }
    public string Cnae { get; private set; }
    public string AtividadePrincipal { get; private set; }
    public string Porte { get; private set; }
    public string RegimeFiscal { get; private set; }
    public string Setor { get; private set; }
}