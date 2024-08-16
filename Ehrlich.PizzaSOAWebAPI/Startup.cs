using Ehrlich.PizzaSOA.Domain.Interfaces.Repositories;
using Ehrlich.PizzaSOA.Infrastructure.Persistence;
using Ehrlich.PizzaSOA.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ehrlich.PizzaSOA.WebAPI;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    // This method is called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        // Configure DbContext with SQL Server (or another provider)
        var connString = Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connString));

        // Repositories
        services.AddTransient<IPizzaTypeRepository, PizzaTypeRepository>();
        services.AddTransient<IPizzaRepository, PizzaRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    // This method is called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        // Ensure the database is created
        EnsureDatabaseCreated(app);
    }

    private static void EnsureDatabaseCreated(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate(); // Applies any pending migrations and creates the database if it does not exist
    }
}
