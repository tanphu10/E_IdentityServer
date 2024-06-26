﻿using EMicroservice.IDP.Extensions;
using EMicroservice.IDP.Persistence;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

var builder = WebApplication.CreateBuilder(args);
try
{

    builder.Host.AddAppConfigurations();
    builder.Host.ConfigureSerilog();

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();
    SeedUserData.EnsureSeedData(builder.Configuration.GetConnectionString("IdentitySqlConnection"));
    app.MigrateDatabase().Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;

    Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
    Log.Information($"Shutdown {builder.Environment.ApplicationName} complete");
    Log.CloseAndFlush();
}