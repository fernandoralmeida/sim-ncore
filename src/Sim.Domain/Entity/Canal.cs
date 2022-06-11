﻿
namespace Sim.Domain.Entity
{
    public  class Canal
    {
        public Canal()
        {

        }
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public Secretaria? Secretaria { get; set; } //Secretaria
        public Setor? Setor { get; set; } //Setor
        public bool Ativo { get; set; }
    }
}