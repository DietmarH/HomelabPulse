WebApplication app = WebApplication.Create(args);

app.MapGet("/", () => "HomelabPulse Web — coming soon.");

await app.RunAsync();
