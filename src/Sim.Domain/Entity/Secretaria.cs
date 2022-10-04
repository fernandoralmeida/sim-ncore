
namespace Sim.Domain.Entity
{
    public class Secretaria
    {
        public Secretaria()
        { }
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Acronimo {get ; set;}
        public EPrefeitura? Owner { get; set; }
        public bool Ativo { get; set; }

        public virtual ICollection<Setor>? Setores { get; set; }
        public virtual ICollection<Canal>? Canais { get; set; }
        public virtual ICollection<Servico>? Servicos { get; set; }

    }
}
