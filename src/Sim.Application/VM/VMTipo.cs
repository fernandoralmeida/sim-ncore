using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sim.Application.VM;

public class VMTipo {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [DisplayName("Tipo")]
    public string Nome { get; set; }

    [DisplayName("Tipo")]
    public string Tipo { get; set; } //Tipo

    [DisplayName("Ativo")]
    public bool Ativo { get; set; }
}