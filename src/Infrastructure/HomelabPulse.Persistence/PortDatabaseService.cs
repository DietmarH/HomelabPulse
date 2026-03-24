using System.Reflection;
using System.Text.Json;
using HomelabPulse.Core.Interfaces;

namespace HomelabPulse.Persistence;

internal sealed class PortDatabaseService : IPortDatabaseService
{
    private readonly IReadOnlyDictionary<string, string> _entries;

    public PortDatabaseService() => _entries = Load();

    public string? GetServiceName(int port, string protocol)
    {
        var key = $"{port}/{protocol.ToLowerInvariant()}";
        return _entries.TryGetValue(key, out var name) ? name : null;
    }

    private static IReadOnlyDictionary<string, string> Load()
    {
        var assembly = Assembly.GetExecutingAssembly();
        const string resourceName = "HomelabPulse.Persistence.Resources.ports.json";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream is null)
        {
            var availableResources = string.Join(", ", assembly.GetManifestResourceNames());
            throw new InvalidOperationException(
                $"Embedded resource '{resourceName}' was not found. Available resources: {availableResources}");
        }
        return JsonSerializer.Deserialize<Dictionary<string, string>>(stream)
            ?? throw new InvalidOperationException("Failed to load embedded ports.json.");
    }
}
