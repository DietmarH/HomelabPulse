using HomelabPulse.Core.Configuration;
using HomelabPulse.Integrations.Kubernetes;
using HomelabPulse.Integrations.Synology;
using HomelabPulse.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace HomelabPulse.Desktop;

internal static class AppBootstrapper
{
    internal static void ConfigureServices(IServiceCollection services, HomelabPulseOptions options)
    {
        if (options.BackendMode == BackendMode.Api)
        {
            // Remote mode: all calls go through the HTTP API
            // services.AddHomelabPulseApiClient(options.ApiClient.BaseAddress);
        }
        else
        {
            // Direct mode: platform integrations run in-process
            services.AddSynologyIntegration(options.Synology);
            services.AddKubernetesIntegration(options.Kubernetes);
            services.AddHomelabPulsePersistence();
        }
    }
}
