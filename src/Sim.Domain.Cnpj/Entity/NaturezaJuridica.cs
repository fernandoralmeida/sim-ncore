namespace Sim.Domain.Cnpj.Entity
{
    public class NaturezaJuridica
    {
        public NaturezaJuridica()
        {

        }
        public NaturezaJuridica(string codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }
        public string Codigo { get; private set; }
        /// <summary>
        /// Nome da naturza jurídica
        /// </summary>
        public string Descricao { get; private set; }
    }
}
