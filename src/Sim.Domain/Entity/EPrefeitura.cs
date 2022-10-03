namespace Sim.Domain.Entity;
public class EPrefeitura {
    public EPrefeitura() {        
    }

    public EPrefeitura (Guid id, string? nome, string? cidade, string uf, bool ativo) {
        Id = id;
        Nome = nome;
        Cidade = cidade;
        UF = uf;
        Ativo = ativo;
    }

    public Guid Id { get; private set; }
    public string? Nome { get; private set; }
    public string? Cidade { get; private set; } 
    public string? UF { get; private set; } 
    public bool Ativo { get; set; }

    public virtual ICollection<Secretaria>? Secretarias { get; set; }
}