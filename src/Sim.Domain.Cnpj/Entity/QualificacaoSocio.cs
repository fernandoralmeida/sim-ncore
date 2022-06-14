namespace Sim.Domain.Cnpj.Entity
{
    public class QualificacaoSocio
    {
        public QualificacaoSocio()
        {

        }
        public QualificacaoSocio(string codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }
        public string Codigo { get; private set; }
        /// <summary>
        /// Nome da qualificacao do Sócio
        /// </summary>
        public string Descricao { get; private set; }
    }
}
