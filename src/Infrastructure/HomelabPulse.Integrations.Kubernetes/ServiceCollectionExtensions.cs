using HomelabPulse.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomelabPulse.Integrations.Kubernetes;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKubernetesIntegration(
        this IServiceCollection services,
        KubernetesOptions options)
    {
        // TODO: Register KubernetesNodeDiscoveryService and KubernetesScanOrchestrator
        _ = options;
        return services;
    }
}
