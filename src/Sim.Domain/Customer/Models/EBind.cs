using Sim.Domain.Entity;

namespace Sim.Domain.Customer.Models;

public class EBind
{    
    public EBind()
    { }
    public Guid Id { get; set; }
    public TBindings Vinculo { get; set; }
    public Pessoa? Pessoa { get; set; }
    public Empresas? Empresa { get; set; }           
}