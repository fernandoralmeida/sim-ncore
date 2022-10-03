using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sim.Domain.Entity;

namespace Sim.Application.VM;
public class VMPrefeitura {

    [Key]
    public Guid Id { get; set; }

    [Required]
    [DisplayName("Organização")]
    public string Nome { get; set; }

    [DisplayName("Município")]
    public string Cidade { get; set; } 
    
    [DisplayName("UF")]
    public string UF { get; set; } 

    [DisplayName("Ativo")]
    public bool Ativo { get; set; }

    public virtual ICollection<EPrefeitura> Listar { get; set; }

}