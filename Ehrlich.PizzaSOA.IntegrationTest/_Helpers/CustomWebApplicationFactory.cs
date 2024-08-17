using Ehrlich.PizzaSOA.Application.Services;
using Ehrlich.PizzaSOA.Domain.Constants;
using Ehrlich.PizzaSOA.Domain.Entities;
using Ehrlich.PizzaSOA.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static Ehrlich.PizzaSOA.Domain.Constants.Rules;

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

            // Seeding data

            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();

            CustomWebApplicationFactory<TProgram>.SeedDatabase(dbContext);
        });
    }

    private static void SeedDatabase(ApplicationDbContext dbContext)
    {
        // We add any initial data for these tests

        var pizzaTypes = new List<PizzaType>
        {
            new() { PizzaTypeCode = "bbq_ckn", Name = "The Barbecue Chicken Pizza", Category = Enum.Parse<Rules.PizzaTypeCategoriesEnum>("Chicken"), Ingredients = "Barbecued Chicken, Red Peppers, Green Peppers, Tomatoes, Red Onions, Barbecue Sauce" },
            new() { PizzaTypeCode = "cali_ckn", Name = "The California Chicken Pizza", Category = Enum.Parse<Rules.PizzaTypeCategoriesEnum>("Chicken"), Ingredients = "Chicken, Artichoke, Spinach, Garlic, Jalapeno Peppers, Fontina Cheese, Gouda Cheese" },
            new() { PizzaTypeCode = "ckn_alfredo", Name = "The Chicken Alfredo Pizza", Category = Enum.Parse<Rules.PizzaTypeCategoriesEnum>("Chicken"), Ingredients = "Chicken, Red Onions, Red Peppers, Mushrooms, Asiago Cheese, Alfredo Sauce" },
            new() { PizzaTypeCode = "ckn_pesto", Name = "The Chicken Pesto Pizza", Category = Enum.Parse<Rules.PizzaTypeCategoriesEnum>("Chicken"), Ingredients = "Chicken, Tomatoes, Red Peppers, Spinach, Garlic, Pesto Sauce" },
            new() { PizzaTypeCode = "southw_ckn", Name = "The Southwest Chicken Pizza", Category = Enum.Parse<Rules.PizzaTypeCategoriesEnum>("Chicken"), Ingredients = "Chicken, Tomatoes, Red Peppers, Red Onions, Jalapeno Peppers, Corn, Cilantro, Chipotle Sauce" },
            new() { PizzaTypeCode = "thai_ckn", Name = "The Thai Chicken Pizza", Category = Enum.Parse<Rules.PizzaTypeCategoriesEnum>("Chicken"), Ingredients = "Chicken, Pineapple, Tomatoes, Red Peppers, Thai Sweet Chilli Sauce" },
            new() { PizzaTypeCode = "big_meat", Name = "The Big Meat Pizza", Category = Enum.Parse<Rules.PizzaTypeCategoriesEnum>("Classic"), Ingredients = "Bacon, Pepperoni, Italian Sausage, Chorizo Sausage" },
            new() { PizzaTypeCode = "classic_dlx", Name = "The Classic Deluxe Pizza", Category = Enum.Parse<Rules.PizzaTypeCategoriesEnum>("Classic"), Ingredients = "Pepperoni, Mushrooms, Red Onions, Red Peppers, Bacon" },
            new() { PizzaTypeCode = "hawaiian", Name = "The Hawaiian Pizza", Category = Enum.Parse<Rules.PizzaTypeCategoriesEnum>("Classic"), Ingredients = "Sliced Ham, Pineapple, Mozzarella Cheese" },
            new() { PizzaTypeCode = "ital_cpcllo", Name = "The Italian Capocollo Pizza", Category = Enum.Parse<Rules.PizzaTypeCategoriesEnum>("Classic"), Ingredients = "Capocollo, Red Peppers, Tomatoes, Goat Cheese, Garlic, Oregano" },
            new() { PizzaTypeCode = "napolitana", Name = "The Napolitana Pizza", Category = Enum.Parse<Rules.PizzaTypeCategoriesEnum>("Classic"), Ingredients = "Tomatoes, Anchovies, Green Olives, Red Onions, Garlic" },
            new() { PizzaTypeCode = "pep_msh_pep", Name = "The Pepperoni, Mushroom, and Peppers Pizza", Category = Enum.Parse<Rules.PizzaTypeCategoriesEnum>("Classic"), Ingredients = "Pepperoni, Mushrooms, Green Peppers" },
            new() { PizzaTypeCode = "pepperoni", Name = "The Pepperoni Pizza", Category = Enum.Parse<Rules.PizzaTypeCategoriesEnum>("Classic"), Ingredients = "Mozzarella Cheese, Pepperoni" },
            new() { PizzaTypeCode = "the_greek", Name = "The Greek Pizza", Category = Enum.Parse<Rules.PizzaTypeCategoriesEnum>("Classic"), Ingredients = "Kalamata Olives, Feta Cheese, Tomatoes, Garlic, Beef Chuck Roast, Red Onions" },
            new() { PizzaTypeCode = "brie_carre", Name = "The Brie Carre Pizza", Category = Enum.Parse<Rules.PizzaTypeCategoriesEnum>("Supreme"), Ingredients = "Brie Carre Cheese, Prosciutto, Caramelized Onions, Pears, Thyme, Garlic" },
            new() { PizzaTypeCode = "calabrese", Name = "The Calabrese Pizza", Category = Enum.Parse<Rules.PizzaTypeCategoriesEnum>("Supreme"), Ingredients = "‘Nduja Salami, Pancetta, Tomatoes, Red Onions, Friggitello Peppers, Garlic" },
            new() { PizzaTypeCode = "ital_supr", Name = "The Italian Supreme Pizza", Category = Enum.Parse<Rules.PizzaTypeCategoriesEnum>("Supreme"), Ingredients = "Calabrese Salami, Capocollo, Tomatoes, Red Onions, Green Olives, Garlic" }
        };

        dbContext.PizzaTypes.AddRange(pizzaTypes);
        dbContext.SaveChanges();

        var pizzas = new List<Pizza>();
        var random = new Random();
        var existingPizzaTypes = dbContext.PizzaTypes.ToList();
        List<(Guid PizzaTypeId, string PizzaTypeCode)> pizzaTypeCodes = existingPizzaTypes.Select(p => (p.Id, p.PizzaTypeCode)).ToList();

        foreach (var (pizzaTypeId, pizzaTypeCode) in pizzaTypeCodes)
        {
            foreach (PizzaSizesEnum size in Enum.GetValues(typeof(PizzaSizesEnum)))
            {
                var sizeSfx = size.ToString()?[..1].ToLower();
                var pizzaCode = $"{pizzaTypeCode}_{sizeSfx}";

                var pizza = new Pizza()
                {
                    PizzaCode = pizzaCode,
                    PizzaTypeCode = pizzaTypeCode,
                    PizzaTypeId = pizzaTypeId,
                    Size = size,
                    Price = GetRandomDecimal(5m, 60m)
                };

                pizzas.Add(pizza);
            }
        }

        dbContext.Pizzas.AddRange(pizzas);
        dbContext.SaveChanges();

        var orders = new List<Order>
        {
            new() { OrderNo = 1, DateOrdered = DateTime.Parse("01/01/2015"), TimeOrdered = TimeSpan.Parse("14:03:08") },
            new() { OrderNo = 2, DateOrdered = DateTime.Parse("01/02/2016"), TimeOrdered = TimeSpan.Parse("14:14:29") },
            new() { OrderNo = 3, DateOrdered = DateTime.Parse("01/07/2017"), TimeOrdered = TimeSpan.Parse("14:23:01") },
            new() { OrderNo = 4, DateOrdered = DateTime.Parse("11/07/2018"), TimeOrdered = TimeSpan.Parse("14:19:03") },
            new() { OrderNo = 5, DateOrdered = DateTime.Parse("18/10/2019"), TimeOrdered = TimeSpan.Parse("15:11:17") },
            new() { OrderNo = 6, DateOrdered = DateTime.Parse("06/07/2020"), TimeOrdered = TimeSpan.Parse("14:54:26") },
            new() { OrderNo = 7, DateOrdered = DateTime.Parse("08/09/2021"), TimeOrdered = TimeSpan.Parse("14:16:26") },
            new() { OrderNo = 8, DateOrdered = DateTime.Parse("05/06/2022"), TimeOrdered = TimeSpan.Parse("14:44:44") },
            new() { OrderNo = 9, DateOrdered = DateTime.Parse("12/12/2023"), TimeOrdered = TimeSpan.Parse("15:00:00") },
            new() { OrderNo = 10, DateOrdered = DateTime.Parse("20/12/2022"), TimeOrdered = TimeSpan.Parse("13:30:30") },
            new() { OrderNo = 11, DateOrdered = DateTime.Parse("25/01/2024"), TimeOrdered = TimeSpan.Parse("16:45:00") },
            new() { OrderNo = 12, DateOrdered = DateTime.Parse("15/01/2015"), TimeOrdered = TimeSpan.Parse("14:03:08") },
            new() { OrderNo = 13, DateOrdered = DateTime.Parse("20/03/2016"), TimeOrdered = TimeSpan.Parse("14:14:29") },
            new() { OrderNo = 14, DateOrdered = DateTime.Parse("05/08/2017"), TimeOrdered = TimeSpan.Parse("14:23:01") },
            new() { OrderNo = 15, DateOrdered = DateTime.Parse("22/09/2018"), TimeOrdered = TimeSpan.Parse("14:19:03") },
            new() { OrderNo = 16, DateOrdered = DateTime.Parse("10/11/2019"), TimeOrdered = TimeSpan.Parse("15:11:17") },
            new() { OrderNo = 17, DateOrdered = DateTime.Parse("15/06/2020"), TimeOrdered = TimeSpan.Parse("14:54:26") },
            new() { OrderNo = 18, DateOrdered = DateTime.Parse("25/08/2021"), TimeOrdered = TimeSpan.Parse("14:16:26") },
            new() { OrderNo = 19, DateOrdered = DateTime.Parse("30/11/2022"), TimeOrdered = TimeSpan.Parse("14:44:44") },
            new() { OrderNo = 20, DateOrdered = DateTime.Parse("01/01/2024"), TimeOrdered = TimeSpan.Parse("15:00:00") },
            new() { OrderNo = 21, DateOrdered = DateTime.Parse("15/02/2024"), TimeOrdered = TimeSpan.Parse("13:30:30") },
            new() { OrderNo = 22, DateOrdered = DateTime.Parse("05/03/2024"), TimeOrdered = TimeSpan.Parse("16:45:00") }
        };

        dbContext.Orders.AddRange(orders);
        dbContext.SaveChanges();


        var existingOrders = dbContext.Orders.ToList();
        var existingPizzas = dbContext.Pizzas.ToList();
        var orderDetails = new List<OrderDetail>();

        for (var ctr = 0; ctr < 100; ctr++)
        {

            var orderItem = existingOrders[random.Next(existingOrders.Count)];
            var pizzaItem = existingPizzas[random.Next(existingPizzas.Count)];

            orderDetails.Add(
                new OrderDetail
                {
                    OrderDetailNo = ctr + 1,
                    OrderId = orderItem.Id,
                    PizzaId = pizzaItem.Id,
                    Quantity = random.Next(10)
                });
        }

        dbContext.OrderDetails.AddRange(orderDetails);
        dbContext.SaveChanges();
    }


    internal static decimal GetRandomDecimal(decimal minValue, decimal maxValue)
    {
        Random random = new();
        double range = (double)(maxValue - minValue);
        double sample = random.NextDouble();
        decimal scaled = (decimal)(sample * range);
        return minValue + scaled;
    }
}