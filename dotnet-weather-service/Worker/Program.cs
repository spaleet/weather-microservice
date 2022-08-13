using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, lc) =>
            lc.ReadFrom.Configuration(context.Configuration));

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.Run();