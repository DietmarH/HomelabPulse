using HomelabPulse.Core.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace HomelabPulse.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHomelabPulsePersistence(
        this IServiceCollection services,
        Action<PersistenceOptions>? configure = null)
    {
        var options = new PersistenceOptions();
        configure?.Invoke(options);

        services
            .AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(options.DataDirectory, "keys")));

        services.AddSingleton<ICredentialStore>(sp => new CredentialStore(
            sp.GetRequiredService<IDataProtectionProvider>(),
            Path.Combine(options.DataDirectory, "credentials.json")));

        services.AddSingleton<IHostProfileRepository>(_ => new HostProfileRepository(
            Path.Combine(options.DataDirectory, "profiles.json")));

        services.AddSingleton<IPortDatabaseService, PortDatabaseService>();

        return services;
    }
}
