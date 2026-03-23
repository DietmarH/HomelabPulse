namespace HomelabPulse.Core.Interfaces;

public interface ICredentialStore
{
    Task<string?> GetAsync(string credentialId, CancellationToken ct = default);
    Task SetAsync(string credentialId, string secret, CancellationToken ct = default);
    Task DeleteAsync(string credentialId, CancellationToken ct = default);
}
