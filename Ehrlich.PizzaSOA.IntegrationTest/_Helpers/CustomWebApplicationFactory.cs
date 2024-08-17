using Ehrlich.PizzaSOA.Domain.Entities;
using Ehrlich.PizzaSOA.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ehrlich.PizzaSOA.IntegrationTest.Helpers;

/// <summary>
/// Reason for using this class is to create a test server that mimics the production environment, 
/// allowing the test of the API endpoints in a controlled, isolated setting. 
/// Use of the default Test Server Factory, WebApplicationFactory<Startup> default to AspNetCore.MVC.Testing assembly 
/// recently throws an issue where the connection string is needed present in the test project wich is not ideal.
/// </summary>
public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the app's database context registration
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            // Add an in-memory database for testing
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            // Just an option seeding the database with test data
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();

            // Seed test data here if necessary
            CustomWebApplicationFactory<TProgram>.SeedDatabase(dbContext);
        });
    }

    private static void SeedDatabase(ApplicationDbContext dbContext)
    {
        // We add any initial data for these tests
        var orders = new List<Order> {
            new() { OrderNo = 20, DateOrdered = DateTime.Parse("01/01/2015"), TimeOrdered = TimeSpan.Parse("14:03:08") },
            new() { OrderNo = 21, DateOrdered = DateTime.Parse("01/01/2015"), TimeOrdered = TimeSpan.Parse("14:14:29") },
            new() { OrderNo = 22, DateOrdered = DateTime.Parse("01/01/2015"), TimeOrdered = TimeSpan.Parse("14:16:26") },
            new() { OrderNo = 23, DateOrdered = DateTime.Parse("01/01/2015"), TimeOrdered = TimeSpan.Parse("14:19:03") },
            new() { OrderNo = 24, DateOrdered = DateTime.Parse("01/01/2015"), TimeOrdered = TimeSpan.Parse("14:23:01") },
            new() { OrderNo = 25, DateOrdered = DateTime.Parse("01/01/2015"), TimeOrdered = TimeSpan.Parse("14:44:44") },
            new() { OrderNo = 26, DateOrdered = DateTime.Parse("01/01/2015"), TimeOrdered = TimeSpan.Parse("14:54:26") },
            new() { OrderNo = 27, DateOrdered = DateTime.Parse("01/01/2015"), TimeOrdered = TimeSpan.Parse("15:11:17") }
        };

        dbContext.Orders.AddRange(orders);
        dbContext.SaveChanges();
    }
}