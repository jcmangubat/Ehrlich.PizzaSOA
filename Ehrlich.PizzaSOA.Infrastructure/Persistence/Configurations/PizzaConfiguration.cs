using Ehrlich.PizzaSOA.Domain.Constants;
using Ehrlich.PizzaSOA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using SMEAppHouse.Core.Patterns.EF.ModelCfgAbstractions;

namespace Ehrlich.PizzaSOA.Infrastructure.Persistence.Configurations;

public class PizzaConfiguration(string schema = "dbo")
    : EntityConfiguration<Pizza, Guid>(prefixEntityNameToId: true,
        prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<Pizza> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(x => x.PizzaItemCode, true, FieldLengths.General.LENGTH20);
        entityBuilder.DefineDbField(x => x.Size, true);
        entityBuilder.DefineDbField(x => x.Price, true, "decimal(18,2)");

        entityBuilder.HasOne(p => p.PizzaType)
                        .WithMany(p => p.Pizzas)
                        .HasForeignKey(p => p.PizzaTypeId)
                        .IsRequired();
    }

    public override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configures precision and scale to fix EF issue warning where
        // Price field does not have a specified SQL Server column type, precision or
        // scale that might result to data truncation or rounding issues.
        modelBuilder.Entity<Pizza>()
                    .Property(p => p.Price)
                    .HasColumnType("decimal(18,2)")
                    .HasPrecision(18, 2);
    }
}
