namespace Sim.Domain.Entity
{
    public class BIAtendimentos
    {
        public (string Titulo, int Atendimentos, int Servicos) Cliente { get; set; }
        public (string Titulo, int Atendimentos, int Servicos) ClientePF { get; set; }
        public (string Titulo, int Atendimentos, int Servicos) ClientePJ { get; set; }
        public List<(string Mes, int Atendimentos, int Servicos)>? ListaMensal { get; set; }
        public List<(string Nome, int Atendimentos, int Servicos)>? ListaAppUser { get; set; }
        public List<(string Servico, int Quantidade)>? ListaServicos { get; set; }
    }
}
