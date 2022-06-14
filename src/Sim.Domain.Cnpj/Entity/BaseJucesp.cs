
namespace Sim.Domain.Cnpj.Entity
{
    public class BaseJucesp
    {
        public BaseJucesp()
        {

        }

        public BaseJucesp(string ie, string cnpj, string nomeempresarial, string nomefantasia, string naturezajuridica,
            string tipologradouro, string nomelogradouro, string numerologradouro, string complementologradouro,
            string cep, string bairro, string municipio, string uf, string situacaocadastral, string datasituacaocadastral,
            string ocorrenciafiscal, string regimeapuracao, string atividadeeconomica)
        {
            Inscricao_Estadual = ie;
            CNPJ = cnpj;
            Nome_Empresarial = nomeempresarial;
            Nome_Fantasia = nomefantasia;
            Natureza_Juridica = naturezajuridica;
            Tipo_Logradouro = tipologradouro;
            Nome_Logradouro = nomelogradouro;
            Numero_Logradouro = numerologradouro;
            Complemento_Logradouro = complementologradouro;
            CEP = cep;
            Bairro = bairro;
            Municipio = municipio;
            UF = uf;
            Situacao_Cadastral = situacaocadastral;
            Data_Situacao_Cadastral = datasituacaocadastral;
            Ocorrencia_Fiscal = ocorrenciafiscal;
            Regime_Apuracao = regimeapuracao;
            Atividade_Economica = atividadeeconomica;
        }

        public string Inscricao_Estadual { get; private set; }
        public string CNPJ { get; private set; }
        public string Nome_Empresarial { get; private set; }
        public string Nome_Fantasia { get; private set; }
        public string Natureza_Juridica { get; private set; }
        public string Tipo_Logradouro { get; private set; }
        public string Nome_Logradouro { get; private set; }
        public string Numero_Logradouro { get; private set; }
        public string Complemento_Logradouro { get; private set; }
        public string CEP { get; private set; }
        public string Bairro { get; private set; }
        public string Municipio { get; private set; }
        public string UF { get; private set; }
        public string Situacao_Cadastral { get; private set; }
        public string Data_Situacao_Cadastral { get; private set; }
        public string Ocorrencia_Fiscal { get; private set; }
        public string Regime_Apuracao { get; private set; }
        public string Atividade_Economica { get; private set; }
    }
}
