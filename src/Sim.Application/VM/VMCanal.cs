using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
using Sim.Domain.Organizacao.Model;
=======
using Sim.Domain.Entity;
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd

namespace Sim.Application.VM;

public class VMCanal {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [DisplayName("Canal")]
    public string Nome { get; set; }

    [DisplayName("Secretaria")]
<<<<<<< HEAD
    public EOrganizacao Dominio { get; set; } //Secretaria
=======
    public Secretaria Secretaria { get; set; } //Secretaria

    [DisplayName("Setor")]
    public Setor Setor { get; set; } //Setor
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd

    [DisplayName("Ativo")]
    public bool Ativo { get; set; }
}