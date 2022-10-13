
namespace Sim.Domain.Entity
{
    public class Empregos
    {
        public enum EStatus { Ativa = 0, Finalizada = 1, Cancelada = 2 }
        public Empregos() { }
        public Guid Id { get; set; }
        public DateTime? Data { get; set; }
        public string? Ocupacao { get; set; }
        public string? Inclusivo { get; set; }
        public string? Genero { get; set; }
        public string? Experiencia { get; set; }
        public decimal Salario { get; set; }
        public string? Pagamento { get; set; }
        public int Vagas { get; set; }      
        public string? Status { get;set; }
        public virtual Empresas? Empresa { get; set; }
        public virtual Pessoa? Pessoa { get; set; }
    }
}
