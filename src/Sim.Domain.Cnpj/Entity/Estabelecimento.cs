namespace Sim.Domain.Cnpj.Entity
{
    public class Estabelecimento
    {

        public Estabelecimento() { }

        public Estabelecimento(string cnpjbase, string cnpjordem, string dv, string matrizfilial,
            string nomefantasia, string situacaocadastral, string datasituacaocadastral, string motivosc,
            string cidadeexterior, string pais, string datainicioatividade, string cnaeprimario, string cnaesecundarios,
            string tipologradouro, string logradouro, string numero, string complemento, string bairro, string cep,
            string uf, string municipio, string ddd1, string telefone1, string ddd2, string telefone2,
            string dddfax, string fax, string correioeletronico, string situacaoespecial, string datasituacaoespecial) {

            CNPJBase = cnpjbase;
            CNPJOrdem = cnpjordem;
            CNPJDV = dv;
            IdentificadorMatrizFilial = matrizfilial;
            NomeFantasia = nomefantasia;
            SituacaoCadastral = situacaocadastral;
            DataSituacaoCadastral = datasituacaocadastral;
            MotivoSituacaoCadastral = motivosc;
            NomeCidadeExterior = cidadeexterior;
            Pais = pais;
            DataInicioAtividade = datainicioatividade;
            CnaeFiscalPrincipal = cnaeprimario;
            CnaeFiscalSecundaria = cnaesecundarios;
            TipoLogradouro = tipologradouro;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CEP = cep;
            UF = uf;
            Municipio = municipio;
            DDD1 = ddd1;
            DDD2 = ddd2;
            DDDFax = dddfax;
            Telefone1 = telefone1;
            Telefone2 = telefone2;
            Fax = fax;
            CorreioEletronico = correioeletronico;
            SituacaoEspecial = situacaoespecial;
            DataSitucaoEspecial = datasituacaoespecial;
            SetorProdutivo = cnaeprimario;
        }

        public string CNPJBase { get; private set; }
        public string CNPJOrdem { get; private set; }
        public string CNPJDV { get; private set; }

        private string _matrizfilial = string.Empty;
        /// <summary>
        /// 1 para Matriz, 2 Filial.
        /// </summary>
        public string IdentificadorMatrizFilial
        {
            get { return MatrizFilial(_matrizfilial); }
            private set { _matrizfilial = value; }
        }
        public string NomeFantasia { get; private set; }

        private string _getsitucaocadastral = string.Empty;
        /// <summary>
        /// 01 Nula, 02 Ativa, 03 Suspensa, 04 Inapta, 08 Baixada 
        /// </summary>
        public string SituacaoCadastral
        {
            get { return GetSituacaoCadastral(_getsitucaocadastral); }
            private set { _getsitucaocadastral = value; }
        }

        private string _datasc = string.Empty;
        public string DataSituacaoCadastral {
            get { return StringDateTime(_datasc); }

            private set { _datasc = value; }
        }
        public string MotivoSituacaoCadastral { get; private set; }
        public string NomeCidadeExterior { get; private set; }
        public string Pais { get; private set; }

        private string _datainicio = string.Empty;
        public string DataInicioAtividade
        {
            get { return StringDateTime(_datainicio); }

            private set { _datainicio = value; }
        }
        public string CnaeFiscalPrincipal { get; private set; }
        public string CnaeFiscalSecundaria { get; private set; }
        public string TipoLogradouro { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string UF { get; private set; }
        public string Municipio { get; private set; }
        public string DDD1 { get; private set; }
        public string Telefone1 { get; private set; }
        public string DDD2 { get; private set; }
        public string Telefone2 { get; private set; }
        public string DDDFax { get; private set; }
        public string Fax { get; private set; }
        public string CorreioEletronico { get; private set; }
        public string SituacaoEspecial { get; private set; }
        public string DataSitucaoEspecial { get; private set; }
        private string _getsetor = string.Empty;
        public string SetorProdutivo { 
            get { return GetSetor(_getsetor); } 
            
            private set { _getsetor = value;  }
        }

        private string MatrizFilial(string valor)
        {
            if (valor == "1")
                return "Matriz";

            else
                return "Filial";
        }

        private string GetSituacaoCadastral(string valor)
        {
            if (valor == "01")
                return "Nula";

            else if (valor == "02")
                return "Ativa";

            else if (valor == "03")
                return "Suspensa";

            else if (valor == "04")
                return "Inapta";

            else if (valor == "08")
                return "Baixada";

            else
                return valor;
        }

        private string StringDateTime(string valor)
        {
            if (valor == "0")
                valor = _datainicio;


            valor = valor.Insert(4, "-");
            valor = valor.Insert(7, "-");
            return valor;
        }

        private string GetSetor(string valor) {
            try {
                var cnae = Convert.ToInt32(valor.Remove(2, 5));

                if (cnae >= 1 && cnae <= 3)
                    return "Agro";

                else if (cnae >= 45 && cnae <= 47)
                    return "Comércio";

                else if (cnae >= 05 & cnae <= 09 || cnae >= 10 && cnae <= 33)
                    return "Indústria";

                else if (cnae >= 41 & cnae <= 43)
                    return "Construção";

                else if (cnae == 35 || (cnae >= 36 && cnae <= 39)
                    || (cnae >= 49 && cnae <= 53)
                    || (cnae >= 55 && cnae <= 56)
                    || (cnae >= 58 && cnae <= 63)
                    || (cnae >= 64 && cnae <= 66)
                    || (cnae == 68)
                    || (cnae >= 69 && cnae <= 75)
                    || (cnae >= 77 && cnae <= 82)
                    || (cnae == 85)
                    || (cnae >= 86 && cnae <= 88)
                    || (cnae >= 86 && cnae <= 88)
                    || (cnae >= 90 && cnae <= 93)
                    || (cnae >= 94 && cnae <= 96)
                    || (cnae == 97)
                    || (cnae == 99))
                    return "Serviços";
                else
                    return "*";
            }
            catch(Exception ex) {
                return ex.Message;
            }            
        }
    }
}
