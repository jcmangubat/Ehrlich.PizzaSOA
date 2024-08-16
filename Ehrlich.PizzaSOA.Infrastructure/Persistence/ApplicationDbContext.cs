using Ehrlich.PizzaSOA.Domain.Entities;
using Ehrlich.PizzaSOA.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using SMEAppHouse.Core.Patterns.EF.SettingsModel;
namespace Ehrlich.PizzaSOA.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, DbMigrationInformation? dbMigrationInformation = null) 
    : DbContext(options)
{
    private readonly DbMigrationInformation _dbMigrationInformation = dbMigrationInformation;

    public virtual DbSet<PizzaType> PizzaTypes { get; set; }
    public virtual DbSet<Pizza> Pizzas { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var dbSchema = "dbo";
        builder.HasDefaultSchema(dbSchema);

        if (_dbMigrationInformation != null
            && !string.IsNullOrEmpty(_dbMigrationInformation.DbSchema))
        {
            dbSchema = _dbMigrationInformation.DbSchema;
            builder.HasDefaultSchema(_dbMigrationInformation.DbSchema);
        }

        builder.ApplyConfiguration(new PizzaTypeConfiguration(dbSchema));
        builder.ApplyConfiguration(new PizzaConfiguration(dbSchema));
        builder.ApplyConfiguration(new OrderConfiguration(dbSchema));
        builder.ApplyConfiguration(new OrderDetailConfiguration(dbSchema));

        /*new DataSeeder(builder)
            .SeedRolesAndUsers()
            .SeedQuestionAndAnswers()
            .SeedArticleCategory()
            .SeedArticlePostEntity()
            .SeedTestimoniesEntity()
            .SeedFeatureProjectsEntity();*/
    }
}
