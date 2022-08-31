using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddUserSecrets<Program>();

builder.Services.ConfigureServices(builder.Configuration);

builder.Host.UseSerilog((context, lc) => lc.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

await app.RunAsync();