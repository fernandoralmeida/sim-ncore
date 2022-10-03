using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sim.Domain.Entity;

namespace Sim.Application.VM;

public class VMSetor {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [DisplayName("Setor")]
    public string Nome { get; set; }

    [DisplayName("Secretaria")]
    public Secretaria Secretaria { get; set; } //Secretaria

    [DisplayName("Ativo")]
    public bool Ativo { get; set; }
    public virtual ICollection<Canal> Canais { get; set; }
    public virtual ICollection<Servico> Servicos { get; set; }
}