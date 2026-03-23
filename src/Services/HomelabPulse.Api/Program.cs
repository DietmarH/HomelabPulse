using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

    builder.Services.AddOpenApi();

    // TODO: Register infrastructure services
    // builder.Services.AddSynologyIntegration(...)
    // builder.Services.AddKubernetesIntegration(...)
    // builder.Services.AddHomelabPulsePersistence()

    WebApplication app = builder.Build();

    if (app.Environment.IsDevelopment())
        app.MapOpenApi();

    app.UseHttpsRedirection();

    // TODO: Map endpoint groups
    // app.MapGroup("/api/nodes").MapNodesEndpoints()
    // app.MapGroup("/api/scans").MapScansEndpoints()

    await app.RunAsync();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    await Log.CloseAndFlushAsync();
}
