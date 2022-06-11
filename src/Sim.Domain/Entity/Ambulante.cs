
namespace Sim.Domain.Entity
{   
    public class Ambulante
    {
        public Ambulante()
        {

        }
        public Guid Id { get; set; }
        public string? Protocolo { get; set; }
        public string? FormaAtuacao { get; set; }
        public string? Local { get; set; }
        public string? Atividade { get; set; }
        public DateTime? Data_Cadastro { get; set; }
        public DateTime? Ultima_Alteracao { get; set; }
        public bool Ativo { get; set; }

        //relacionais
        public virtual ICollection<Pessoa>? Pessoas { get; set; }
        public virtual ICollection<DIA>? DIAs { get; set; }
    }
}
