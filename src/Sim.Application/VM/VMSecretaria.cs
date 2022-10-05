using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sim.Domain.Organizacao.Model;

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
    public EHierarquia Hierarquia { get; set; } //Prefeitura
    
    [DisplayName("Dominio")]
    public string Dominio { get; set; } //Prefeitura

    [DisplayName("Ativo")]
    public bool Ativo { get; set; }

    public virtual ICollection<ECanal> Canais { get; set; }
    public virtual ICollection<EServico> Servicos { get; set; }
}