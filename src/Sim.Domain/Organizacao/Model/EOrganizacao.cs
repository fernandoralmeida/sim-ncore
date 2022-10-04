
namespace Sim.Domain.Organizacao.Model
{
    public enum EHierarquia { Matriz = 0, Secretaria = 1, Setor = 2 }
    public class EOrganizacao
    {
        public EOrganizacao()
        { }
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Acronimo {get ; set;}
        public EHierarquia? Hierarquia { get; set; }
        public string? Dominio { get; set;}
        public bool Ativo { get; set; }

        public virtual ICollection<ECanal>? Canais { get; set; }
        public virtual ICollection<EServico>? Servicos { get; set; }
    }
}
