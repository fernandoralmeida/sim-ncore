namespace Sim.Domain.Cnpj.Entity
{
    public class Socio
    {
        public Socio()
        {

        }
        public Socio(string cnpj, string identificadorsocio, string nomerazaosocio, string cnpjcpfsocio,
            string qualificacaosocio, string dataentradasociedade, string pais, string cpfreplegal,
            string nomereplegal, string qualreplegal, string faixaetaria)
        {
            CNPJBase = cnpj;
            IdentificadorSocio = identificadorsocio;
            NomeRazaoSocio = nomerazaosocio;
            CnpjCpfSocio = cnpjcpfsocio;
            QualificacaoSocio = qualificacaosocio;
            DataEntradaSociedade = dataentradasociedade;
            Pais = pais;
            RepresentanteLegal = cpfreplegal;
            NomeRepresentante = nomereplegal;
            QualificacaoRepresentanteLegal = qualreplegal;
            FaixaEtaria = faixaetaria;
        }
        public string CNPJBase { get; private set; }

        private string _identificadorsocio = string.Empty;
        /// <summary>
        /// 1 Pessoa Juridica, 2 Pessoa Fisica, 3 Estrangeiro
        /// </summary>
        public string IdentificadorSocio
        {
            get { return GetIdentificacao(_identificadorsocio); }
            private set { _identificadorsocio = value; }
        }
        public string NomeRazaoSocio { get; private set; }
        public string CnpjCpfSocio { get; private set; }
        public string QualificacaoSocio { get; private set; }
        public string DataEntradaSociedade { get; private set; }
        /// <summary>
        /// Codigo do País
        /// </summary>
        public string Pais { get; private set; }
        /// <summary>
        /// CPF
        /// </summary>
        public string RepresentanteLegal { get; private set; }
        public string NomeRepresentante { get; private set; }
        public string QualificacaoRepresentanteLegal { get; private set; }

        private string _faixaetaria = string.Empty;
        /// <summary>
        /// 1 - 0 a 12 anos
        /// 2 - 13 a 20 anos
        /// 3 - 21 a 30 anos
        /// 4 - 31 a 40 anos
        /// 5 - 41 a 50 anos
        /// 6 - 51 a 60 anos
        /// 7 - 61 a 70 anos
        /// 8 - 71 a 80 anos
        /// 9 - Maior de 80 anos
        /// 0 - Nã0 se aplica
        /// </summary>
        public string FaixaEtaria
        {
            get { return GetFaixaEtaria(_faixaetaria); }
            private set { _faixaetaria = value; }
        }

        private string GetIdentificacao(string valor)
        {
            if (valor == "1")
                return "Pessoa Jurídica";

            else if (valor == "2")
                return "Pessoa Física";

            else if (valor == "3")
                return "Estrangeiro";

            else
                return valor;
        }

        private string GetFaixaEtaria(string valor)
        {
            if (valor == "1")
                return "0 a 12 anos";

            else if (valor == "2")
                return "13 a 20 anos";

            else if (valor == "3")
                return "21 a 30 anos";

            else if (valor == "4")
                return "31 a 40 anos";

            else if (valor == "5")
                return "41 a 50 anos";

            else if (valor == "6")
                return "51 a 60 anos";

            else if (valor == "7")
                return "61 a 70 anos";

            else if (valor == "8")
                return "71 a 80 anos";

            else if (valor == "9")
                return "Maior de 80 anos";

            else if (valor == "0")
                return "Não se aplica";

            else
                return valor;
        }
    }
}
