using Ehrlich.PizzaSOA.Application.Interfaces;
using Ehrlich.PizzaSOA.Application.Services;
using Ehrlich.PizzaSOA.Domain.Interfaces.Repositories;
using Ehrlich.PizzaSOA.Infrastructure.Persistence;
using Ehrlich.PizzaSOA.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Ehrlich.PizzaSOA.WebAPI;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    // This method is called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Configure DbContext with SQL Server (or another provider)
        var connString = Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connString));

        // Repositories
        services.AddTransient<IPizzaTypeRepository, PizzaTypeRepository>();
        services.AddTransient<IPizzaRepository, PizzaRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();

        // Services
        services.AddTransient<IPizzaTypeService, PizzaTypeService>();

        // Add AutoMapper services
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddControllers();

        // Add Swagger services
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ehrlich Pizza Sales Order Analytics Web API", Version = "v1" });
        });
    }

    // This method is called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        // Enable Swagger middleware
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
