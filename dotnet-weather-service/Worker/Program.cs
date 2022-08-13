using Serilog;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.ConfigureServices(hostContext.Configuration);
    })
    .UseSerilog((context, lc) => lc.ReadFrom.Configuration(context.Configuration))
    .Build();

await host.RunAsync();