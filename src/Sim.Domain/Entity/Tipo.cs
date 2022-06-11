
namespace Sim.Domain.Entity
{
    public class Tipo
    {
        public Tipo()
        {

        }
        public Guid Id { get; set; }
        public string? Nome { get; set; } //nome do tipo
        public string? Owner { get; set; } //Tipo
        public bool Ativo { get; set; }
    }
}
