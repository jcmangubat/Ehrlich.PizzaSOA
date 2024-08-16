using Ehrlich.PizzaSOA.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using SMEAppHouse.Core.Patterns.EF.ModelCfgAbstractions;

namespace Ehrlich.PizzaSOA.Infrastructure.Persistence.Configurations;

public class OrderDetailConfiguration(string schema = "dbo")
    : EntityConfiguration<OrderDetail, Guid>(prefixEntityNameToId: true,
        prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<OrderDetail> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(x => x.Quantity, true);

        entityBuilder.HasOne(p => p.Pizza)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(p => p.PizzaId)
                .IsRequired();

        entityBuilder.HasOne(p => p.Order)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(p => p.OrderId)
                .IsRequired();
    }
}