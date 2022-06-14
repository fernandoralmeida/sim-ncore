namespace Sim.Domain.Cnpj.Entity
{
    public class Empresa
    {
        public Empresa()
        {

        }
        public Empresa(string cnpj, string razaosocial,
            string naturezajuridica, string qualresp,
            string capitalsocial, string porte,
            string entefedresp)
        {
            CNPJBase = cnpj;
            RazaoSocial = razaosocial;
            NaturezaJuridica = naturezajuridica;
            QualificacaoResponsavel = qualresp;
            CapitalSocial = capitalsocial;
            PorteEmpresa = porte;
            EnteFederativoResponsavel = entefedresp;
        }
        public string CNPJBase { get; private set; }
        public string RazaoSocial { get; private set; }
        public string NaturezaJuridica { get; private set; }
        public string QualificacaoResponsavel { get; private set; }
        public string CapitalSocial { get; private set; }

        private string _porte = string.Empty;
        /// <summary>
        /// 01 Não Informado,
        /// 02 ME,
        /// 03 EPP,
        /// 05 Demais
        /// </summary>
        public string PorteEmpresa { get { return Porte(_porte); } private set { _porte = value; } }
        public string EnteFederativoResponsavel { get; private set; }
        
        private string Porte(string valor)
        {
            if (valor == "01")
                return "ME";

            else if (valor == "02")
                return "ME";

            else if (valor == "03")
                return "EPP";

            else if (valor == "05")
                return "Demais";
            else
                return valor;
        }
    }
}
