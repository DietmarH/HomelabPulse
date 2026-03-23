using HomelabPulse.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomelabPulse.Integrations.Synology;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSynologyIntegration(
        this IServiceCollection services,
        SynologyOptions options)
    {
        // TODO: Register SynologyNodeDiscoveryService and SynologyPortScanOrchestrator
        _ = options;
        return services;
    }
}
