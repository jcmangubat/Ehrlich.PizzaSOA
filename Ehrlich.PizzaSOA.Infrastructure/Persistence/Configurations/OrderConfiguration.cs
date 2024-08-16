using Ehrlich.PizzaSOA.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using SMEAppHouse.Core.Patterns.EF.ModelCfgAbstractions;

namespace Ehrlich.PizzaSOA.Infrastructure.Persistence.Configurations;

public class OrderConfiguration(string schema = "dbo")
    : EntityConfiguration<Order, Guid>(prefixEntityNameToId: true,
        prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Order> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(x => x.OrderNo, true);
        entityBuilder.DefineDbField(x => x.DateOrdered, true);
        entityBuilder.DefineDbField(x => x.TimeOrdered, true);
    }
}
