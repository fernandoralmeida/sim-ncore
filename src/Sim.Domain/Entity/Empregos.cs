﻿
namespace Sim.Domain.Entity
{
    public class Empregos
    {
        public Empregos() { }
        public Guid Id { get; set; }
        public DateTime? Data { get; set; }
        public string? Ocupacao { get; set; }
        public bool Experiencia { get; set; }
        public decimal Salario { get; set; }
        public string? Pagamento { get; set; }
        public int Vagas { get; set; }      
        public string? Status { get;set; }
        public virtual Empresas? Empresa { get; set; }
    }
}