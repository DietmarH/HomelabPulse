using Microsoft.Extensions.DependencyInjection;

namespace HomelabPulse.ApiClient;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHomelabPulseApiClient(
        this IServiceCollection services,
        Uri baseAddress)
    {
        // TODO: Register ApiNodeDiscoveryService, ApiScanOrchestrator, ApiCredentialStore
        _ = baseAddress;
        return services;
    }
}
