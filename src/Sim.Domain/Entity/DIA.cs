
namespace Sim.Domain.Entity
{
    public class DIA
    {
        public DIA()
        {

        }
        public Guid Id { get; set; }
        public int InscricaoMunicipal { get; set; }
        public string? Autorizacao { get; set; }

        //public string Atividade { get; set; }
        //public string FormaAtuacao { get; set; }
        public string? Veiculo { get; set; }
        public DateTime? Emissao { get; set; }
        public DateTime? Validade { get; set; }
        public string? Processo { get; set; }
        public string? Situacao { get; set; }
        public DateTime? DiaDesde { get; set; }

        //relacional
        public virtual Ambulante? Ambulante { get; set; }
    }

}
