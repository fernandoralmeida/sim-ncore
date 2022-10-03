using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sim.Domain.Entity;

namespace Sim.Application.VM;

public class VMCanal {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [DisplayName("Canal")]
    public string Nome { get; set; }

    [DisplayName("Secretaria")]
    public Secretaria Secretaria { get; set; } //Secretaria

    [DisplayName("Setor")]
    public Setor Setor { get; set; } //Setor

    [DisplayName("Ativo")]
    public bool Ativo { get; set; }
}