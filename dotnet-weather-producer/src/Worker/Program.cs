using Serilog;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(app =>
    {
        app.AddJsonFile("appsettings.json");
        app.AddUserSecrets<Program>();
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.ConfigureServices(hostContext.Configuration);
    })
    .UseSerilog((context, lc) => lc.ReadFrom.Configuration(context.Configuration))
    .Build();

await host.RunAsync();