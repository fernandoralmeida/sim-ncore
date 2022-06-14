namespace Sim.Domain.Cnpj.Entity
{
    public class CNAE
    {
        public CNAE()
        {

        }
        public CNAE(string codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }

        public string Codigo { get; private set; }
        /// <summary>
        /// Nome da Atividade Econômica
        /// </summary>
        public string Descricao { get; private set; }
    }
}
