using dotenv.net;
using Ehrlich.PizzaSOA.WebAPI;
using Serilog;
using Serilog.Events;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        DotEnv.Load();
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .UseSerilog((context, configuration) =>
    {
        configuration.ReadFrom.Configuration(context.Configuration)
                        .WriteTo.Console() // Ensure you have a console sink
                        .WriteTo.File("logs/Ehrlich.PizzaSOA-Logs.txt", rollingInterval: RollingInterval.Day) // File sink configuration
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .MinimumLevel.Override("System", LogEventLevel.Warning)
                        .MinimumLevel.Override("System.Net.Mail", LogEventLevel.Error);
    });

builder.Build().Run();