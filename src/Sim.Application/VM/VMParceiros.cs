using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sim.Domain.Entity;

namespace Sim.Application.VM;

public class VMParceiros {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [DisplayName("Parceiro")]
    public string Nome { get; set; }

    [DisplayName("Secretaria")]
    public Secretaria Secretaria { get; set; } //Secretaria

    [DisplayName("Ativo")]
    public bool Ativo { get; set; }
}