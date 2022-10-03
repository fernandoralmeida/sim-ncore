
namespace Sim.Domain.Entity
{
    public class ETipo
    {
        public ETipo()
        { }
        public Guid Id { get; set; }
        public string? Nome { get; set; } 
        public string? Tipo { get; set; } 
        public Secretaria? Owner { get; set; } 
        public bool Ativo { get; set; }
    }
}
