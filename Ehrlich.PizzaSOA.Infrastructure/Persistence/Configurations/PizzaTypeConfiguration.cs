using Ehrlich.PizzaSOA.Domain.Constants;
using Ehrlich.PizzaSOA.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using SMEAppHouse.Core.Patterns.EF.ModelCfgAbstractions;

namespace Ehrlich.PizzaSOA.Infrastructure.Persistence.Configurations;

public class PizzaTypeConfiguration(string schema = "dbo")
    : EntityConfiguration<PizzaType, Guid>(prefixEntityNameToId: true,
        prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<PizzaType> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(x => x.PizzaTypeCode, true, FieldLengths.General.LENGTH20);
        entityBuilder.DefineDbField(x => x.Category, true);
        entityBuilder.DefineDbField(x => x.Name, true, FieldLengths.General.LENGTH120);
        entityBuilder.DefineDbField(x => x.Ingredients, true, FieldLengths.General.LENGTH500);
    }
}
