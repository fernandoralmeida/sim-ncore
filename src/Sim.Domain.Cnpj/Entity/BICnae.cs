namespace Sim.Domain.Cnpj.Entity
{
    public class BICnae
    {
        public List<CnaeSecao> ListaSecao { get; set; }
    }

    public class CnaeSecao
    {
        public KeyValuePair<string, int> Secao { get; set; }
        public List<CnaeClasse> ListaClasse { get; set; }
    }

    public class CnaeClasse
    {
        public KeyValuePair<string, int> Classe { get; set; }
        public List<KeyValuePair<string, int>> ListaSubClasse { get; set; }
    }
}
