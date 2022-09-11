namespace Sim.Domain.Cnpj.Entity
{
    public class Simples
    {
        public Simples()
        {

        }
        public Simples(string cnpj, string opcsimples, string dataopcsimples,
            string dataexcsimples, string opcmei, string dataopcmei, string dataexcmei)
        {
            CNPJBase = cnpj;
            OpcaoSimples = opcsimples;
            DataOpcaoSimples = dataopcsimples;
            DataExclusaoSimples = dataexcsimples;
            OpcaoMEI = opcmei;
            DataOpcaoMEI = dataopcmei;
            DataExclusaoMEI = dataexcmei;
            RegimeFiscal = GetRegimeFiscal(opcsimples, opcmei);
        }
        public string CNPJBase { get; private set; }

        private string _opcsimples = string.Empty;
        /// <summary>
        /// Sim, Não
        /// </summary>
        public string OpcaoSimples
        {
            get { return GetSimples(_opcsimples); }
            private set { _opcsimples = value; }
        }

        private string _incsim = string.Empty;
        public string DataOpcaoSimples
        {
            get { return StringDateTime(_incsim); }
            private set { _incsim = value; }
        }

        private string _excsim = string.Empty;
        public string DataExclusaoSimples
        {
            get { return StringDateTime(_excsim); }
            private set { _excsim = value; }
        }

        private string _opcmei = string.Empty;
        /// <summary>
        /// Sim, Não
        /// </summary>
        public string OpcaoMEI {
            get { return GetMEI(_opcmei); }
            private set { _opcmei = value; }
        }

        private string _incmei = string.Empty;
        public string DataOpcaoMEI
        {
            get { return StringDateTime(_incmei); }
            private set { _incmei = value; }
        }

        private string _excmei = string.Empty;
        public string DataExclusaoMEI
        {
            get { return StringDateTime(_excmei); }
            private set { _excmei = value; }

        }
        public string RegimeFiscal { get; private set; }

        private string GetSimples(string valor)
        {
            if (valor.ToLower() == "S".ToLower())
                return "Sim";

            else
                return "Não";
        }

        private string GetMEI(string valor)
        {
            if (valor.ToLower() == "S".ToLower())
                return "Sim";

            else
                return "Não";
        }

        private string StringDateTime(string valor)
        {
            valor = valor.Insert(4, "-");
            valor = valor.Insert(7, "-");
            return valor;
        }

        private string GetRegimeFiscal(string valorSN, string valorMEI){
            var _ret = "LRP ou Próprio";
            if (valorSN != null && valorMEI != null) {
                var v1 = valorSN == "Sim" ? "Optante SN" : "LRP ou Próprio";
                _ret = valorMEI == "Sim" ? "Optante SN MEI" : v1;
            }
            return _ret;
        }
    }
}
