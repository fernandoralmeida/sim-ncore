using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sim.Domain.Customer.Models;

namespace Sim.Data.Config.Entity;

public class BindMap : IEntityTypeConfiguration<EBind>
{
    public void Configure(EntityTypeBuilder<EBind> builder)
    {
        builder.HasKey(c => c.Id);        
    }
}