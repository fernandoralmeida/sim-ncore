namespace Sim.Domain.Cnpj.Entity
{
    public class BIEmpresas
    {
        public IEnumerable<(string item, int value, float percent)> Empresas { get; set; }
        public IEnumerable<(string item, int value, float percent)> EmpresasNovas { get; set; }
        public (string item, int value, float percent) Servicos { get; set; }
        public IEnumerable<(string item, int value, float percent)> SubServicos {  get; set; }
        public (string item, int value, float percent) ServicosAnual { get; set; }
        public IEnumerable<(string item, int value, float percent)> SubServicosAnual { get; set; }
        public (string item, int value, float percent) Comercio { get; set; }
        public IEnumerable<(string item, int value, float percent)> SubComercio { get; set; }
        public (string item, int value, float percent) ComercioAnual { get; set; }
        public IEnumerable<(string item, int value, float percent)> SubComercioAnual { get; set; }
        public (string item, int value, float percent) Industria { get; set; }
        public IEnumerable<(string item, int value, float percent)> SubIndustria { get; set; }
        public (string item, int value, float percent) IndustriaAnual { get; set; }
        public IEnumerable<(string item, int value, float percent)> SubIndustriaAnual { get; set; }
        public (string item, int value, float percent) Agro { get; set; }
        public IEnumerable<(string item, int value, float percent)> SubAgro { get; set; }
        public (string item, int value, float percent) AgroAnual { get; set; }
        public IEnumerable<(string item, int value, float percent)> SubAgroAnual { get; set; }
        public (string item, int value, float percent) Construcao { get; set; }
        public IEnumerable<(string item, int value, float percent)> SubConstrucao { get; set; }
        public (string item, int value, float percent) ConstrucaoAnual { get; set; }
        public IEnumerable<(string item, int value, float percent)> SubConstrucaoAnual { get; set; }
        public (string item, int value, float percent) Formalizadas { get; set; }
        public (string item, int value, float percent) Ativas { get; set; }
        public IEnumerable<(string item, int value, float percent)> FormalizadasMensal { get; set; }
        public IEnumerable<(string item, int value, float percent)> AtivasMensal { get; set; }
        public (string item, int value, float percent) Baixadas { get; set; }
        public (string item, int value, float percent) BaixadasAno { get; set; }
        public IEnumerable<(string item, int value, float percent)> BaixadasMensal { get; set; }
        public IEnumerable<(string item, int value, float percent)> Porte { get; set; }
        public IEnumerable<(string item, int value, float percent)> PorteAnual { get; set; }
        public IEnumerable<(string item, int value, float percent)> RFiscal { get; set; }
        public IEnumerable<(string item, int value, float percent)> RFiscalMensal { get; set; }
        public IEnumerable<(string item, int value, float percent)> RFiscalAnual { get; set; }
        public IEnumerable<(string item, int value, float percent)> Maturidade { get; set; }
        public IEnumerable<(string item, int value, float percent)> BaixadasFaixas { get; set; }
        public IEnumerable<(string item, int value)> CnaesTop10 { get; set; }
    }
}


