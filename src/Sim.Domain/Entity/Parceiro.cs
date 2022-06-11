
namespace Sim.Domain.Entity
{
    public class Parceiro
    {
        public Parceiro()
        {

        }

        public Guid Id { get; set; }
        public string? Nome { get; set; } //nome do tipo
        public Secretaria? Secretaria { get; set; } //Tipo
        public bool Ativo { get; set; }
    }
}
