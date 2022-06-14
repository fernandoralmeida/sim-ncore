namespace Sim.Domain.Cnpj.Entity
{
    public class CNAEPrincipal
    {
        public CNAEPrincipal() { }
        public CNAEPrincipal(string codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }
        public string Codigo { get; private set; }
        /// <summary>
        /// Nome da Atividade Econômica S
        /// </summary>
        public string Descricao { get; private set; }
    }
}
