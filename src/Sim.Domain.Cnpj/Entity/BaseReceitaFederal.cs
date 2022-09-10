namespace Sim.Domain.Cnpj.Entity
{
    public class BaseReceitaFederal
    {
        public BaseReceitaFederal()
        {

        }
        public BaseReceitaFederal(int id, string cnpj, Empresa empresa,
            Estabelecimento estabelecimento, ICollection<Socio> socios,
            Simples simplesnacional, CNAE atividadeprincipal,
            ICollection<CNAESecundaria> atividadesecundarias,
            NaturezaJuridica naturezaJuridica,
            MotivoSituacaoCadastral motivoSituacaoCadastral,
            Municipio municipio,
            QualificacaoSocio qualificacaoSocio)
        {
            Id = id;
            CNPJ = cnpj;
            Empresa = empresa;
            Estabelecimento = estabelecimento;
            Socios = socios;
            SimplesNacional = simplesnacional;
            AtividadePrincipal = atividadeprincipal;
            AtividadeSecundarias = atividadesecundarias;
            NaturezaJuridica = naturezaJuridica;
            MotivoSituacaoCadastral = motivoSituacaoCadastral;
            Cidade = municipio;
            QualificacaoSocio = qualificacaoSocio;
        }
        public int Id { get; private set; }
        public string CNPJ { get; private set; }
        public virtual Empresa Empresa { get; private set; }
        public virtual Estabelecimento Estabelecimento { get; private set; }
        public virtual ICollection<Socio> Socios { get; private set; }
        public virtual Simples SimplesNacional { get; private set; }
        public virtual CNAE AtividadePrincipal { get; private set; }
        public virtual ICollection<CNAESecundaria> AtividadeSecundarias { get; private set; }
        public virtual NaturezaJuridica NaturezaJuridica { get; private set; }
        public virtual MotivoSituacaoCadastral MotivoSituacaoCadastral { get; private set; }
        public virtual Municipio Cidade { get; private set; }
        public virtual QualificacaoSocio QualificacaoSocio { get; private set; }

        public bool EmpresaNula(BaseReceitaFederal obj)
        {
            return obj.Estabelecimento.SituacaoCadastral == "Nula";
        }

        public bool EmpresaAtiva(BaseReceitaFederal obj)
        {
            return obj.Estabelecimento.SituacaoCadastral == "Ativa";
        }

        public bool EmpresaSuspensa(BaseReceitaFederal obj)
        {
            return obj.Estabelecimento.SituacaoCadastral == "Suspensa"; 
        }

        public bool EmpresaInapta(BaseReceitaFederal obj)
        {
            return obj.Estabelecimento.SituacaoCadastral == "Inapta"; 
        }
        
        public bool EmpresaBaixada(BaseReceitaFederal obj)
        {
            return obj.Estabelecimento.SituacaoCadastral == "Baixada"; 
        }

        public bool OptanteSimplesNacional(BaseReceitaFederal obj)
        {
            return obj.SimplesNacional != null && obj.SimplesNacional.OpcaoSimples == "Sim";
        }
        public bool OptanteMEI(BaseReceitaFederal obj)
        {
            return obj.SimplesNacional != null && obj.SimplesNacional.OpcaoMEI == "Sim";
        }

        public bool OptanteSimplesNacionalNaoMEI (BaseReceitaFederal obj)
        {
            return obj.SimplesNacional != null && obj.SimplesNacional.OpcaoSimples == "Sim" && obj.SimplesNacional.OpcaoMEI == "Não";
        }
        public bool ExclusaoSimplesNacional(BaseReceitaFederal obj)
        {
            return obj.SimplesNacional != null && obj.SimplesNacional.OpcaoSimples == "Não";
        }
        public bool ExclusaoSimplesNacionalMEI(BaseReceitaFederal obj)
        {
            return obj.SimplesNacional != null && obj.SimplesNacional.DataExclusaoMEI != "0000-00-00";
        }

        public bool EmpresasPorRuas(BaseReceitaFederal obj, string rua) =>
            obj.Estabelecimento.Logradouro == rua;

    }
}
