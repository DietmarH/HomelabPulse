using Microsoft.Extensions.DependencyInjection;

namespace HomelabPulse.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHomelabPulsePersistence(this IServiceCollection services)
    {
        // TODO: Register CredentialStore, HostProfileRepository, PortDatabase
        return services;
    }
}
