namespace Sim.Domain.Cnpj.Entity
{
    public class MotivoSituacaoCadastral
    {
        public MotivoSituacaoCadastral()
        {

        }
        public MotivoSituacaoCadastral(string codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }
        public string Codigo
        {
            get; private set;
        }
        public string Descricao
        {
            get; private set;
         }
    }
}
