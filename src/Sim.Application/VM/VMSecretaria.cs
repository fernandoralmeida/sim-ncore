using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
using Sim.Domain.Organizacao.Model;
=======
using Sim.Domain.Entity;
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd

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
<<<<<<< HEAD
    public EHierarquia Hierarquia { get; set; } //Prefeitura
    
    [DisplayName("Dominio")]
    public string Dominio { get; set; } //Prefeitura
=======
    public EPrefeitura Owner { get; set; } //Prefeitura
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd

    [DisplayName("Ativo")]
    public bool Ativo { get; set; }

<<<<<<< HEAD
    public virtual ICollection<ECanal> Canais { get; set; }
    public virtual ICollection<EServico> Servicos { get; set; }
=======
    public virtual ICollection<Setor> Setores { get; set; }
    public virtual ICollection<Canal> Canais { get; set; }
    public virtual ICollection<Servico> Servicos { get; set; }
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
}