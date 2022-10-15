using Sim.Domain.Cnpj.Entity;

namespace Sim.Application.Cnpj.Views;
public class VMBaseReceitaFederal {
    public int Id { get; set; }
    public string? CNPJ { get; set; }
    public virtual Empresa? Empresa { get; set; }
    public virtual Estabelecimento? Estabelecimento { get; set; }
    public virtual ICollection<Socio>? Socios { get; set; }
    public virtual Simples? SimplesNacional { get; set; }
    public virtual CNAE? AtividadePrincipal { get; set; }
    public virtual ICollection<CNAESecundaria>? AtividadeSecundarias { get; set; }
    public virtual NaturezaJuridica? NaturezaJuridica { get; set; }
    public virtual MotivoSituacaoCadastral? MotivoSituacaoCadastral { get; set; }
    public virtual Municipio? Cidade { get; set; }
    public virtual QualificacaoSocio? QualificacaoSocio { get; set; }
}