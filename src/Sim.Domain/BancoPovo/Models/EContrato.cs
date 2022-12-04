using Sim.Domain.Entity;

namespace Sim.Domain.BancoPovo.Models;

public class EContrato {

    public enum EnPagamento { Documentacao = 0, Adimplente = 1, Inadimplente = 2, Liquidado = 3 }
    public enum EnSituacao { Documentacao = 0, Aprovado = 1, Reprovado = 2, Cancelado = 3 }
    public EContrato() {

    }

    public Guid Id { get; set; }
    public int Numero { get; set; }
    public DateTime? Data { get ;set; }
    public decimal Valor { get; set; }
    public EnSituacao Situacao { get; set; }  
    public DateTime? DataSituacao { get; set; }
    public Pessoa? Cliente { get; set; }
    public Empresas? Empresa { get; set; }
    public EnPagamento Pagamento { get; set; }
    public string? Descricao { get; set; }
    public string? AppUser { get; set; }
    public DateTime? UltimaAlteracao { get; set; }
    public virtual ICollection<ERenegociacoes>? Renegociacaoes { get; set; }
}