using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sim.Domain.Entity;

namespace Sim.Application.VM;
public class VMSecretaria {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [DisplayName("Secretaria")]
    public string Nome { get; set; }

    [Required]
    [DisplayName("Acrônimo")]
    public string Acronimo { get; set; }

    [DisplayName("Unidade Responsável")]
    public EPrefeitura Owner { get; set; } //Prefeitura

    [DisplayName("Ativo")]
    public bool Ativo { get; set; }

    public virtual ICollection<Setor> Setores { get; set; }
    public virtual ICollection<Canal> Canais { get; set; }
    public virtual ICollection<Servico> Servicos { get; set; }
}