namespace HomelabPulse.Core.Configuration;

public sealed class ApiClientOptions
{
    public Uri BaseAddress { get; set; } = new("http://localhost:5000");
}
