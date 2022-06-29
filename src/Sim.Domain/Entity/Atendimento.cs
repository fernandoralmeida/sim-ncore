
namespace Sim.Domain.Entity
{

    public class Atendimento
    {
        public Atendimento()
        {

        }
        public Guid Id { get; set; }
        public string? Protocolo { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? DataF { get; set; }
        public string? Setor { get; set; }
        public string? Canal { get; set; }
        public string? Servicos { get; set; }
        public string? Descricao { get; set; }
        public string? Status { get; set; }
        public DateTime? Ultima_Alteracao { get; set; }
        public bool Ativo { get; set; }
        public string? Owner_AppUser_Id { get; set; }
        public virtual Pessoa? Pessoa { get; set; }
        public virtual Empresas? Empresa { get; set; }
        public virtual RaeSebrae? Sebrae { get; set; }

        public bool RaeLancados(Atendimento obj)
        {
            return obj.Sebrae != null && obj.Status != "Cancelado" && obj.Setor == "Sebrae Aqui" && obj.Data.Value.Year == DateTime.Now.Year;
        }

        public bool RaeNaoLancados(Atendimento obj)
        {
            return obj.Sebrae == null || obj.Sebrae.RAE == "" && obj.Status != "Cancelado" && obj.Setor == "Sebrae Aqui" && obj.Data.Value.Year == DateTime.Now.Year;
        }

        public bool BySetor(Atendimento obj, string setor_name)
        {
            return obj.Setor == setor_name;
        }
    }
}
