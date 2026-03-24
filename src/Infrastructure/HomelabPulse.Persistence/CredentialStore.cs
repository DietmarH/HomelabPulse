using System.Text.Json;
using HomelabPulse.Core.Interfaces;
using Microsoft.AspNetCore.DataProtection;

namespace HomelabPulse.Persistence;

internal sealed class CredentialStore : ICredentialStore
{
    private readonly IDataProtector _protector;
    private readonly string _filePath;
    private readonly SemaphoreSlim _lock = new(1, 1);

    public CredentialStore(IDataProtectionProvider protection, string filePath)
    {
        _protector = protection.CreateProtector("HomelabPulse.Credentials.v1");
        _filePath = filePath;
    }

    public async Task<string?> GetAsync(string credentialId, CancellationToken ct = default)
    {
        await _lock.WaitAsync(ct);
        try
        {
            var store = await ReadAsync(ct);
            return store.TryGetValue(credentialId, out var encrypted)
                ? _protector.Unprotect(encrypted)
                : null;
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task SetAsync(string credentialId, string secret, CancellationToken ct = default)
    {
        await _lock.WaitAsync(ct);
        try
        {
            var store = await ReadAsync(ct);
            store[credentialId] = _protector.Protect(secret);
            await WriteAsync(store, ct);
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task DeleteAsync(string credentialId, CancellationToken ct = default)
    {
        await _lock.WaitAsync(ct);
        try
        {
            var store = await ReadAsync(ct);
            if (store.Remove(credentialId))
                await WriteAsync(store, ct);
        }
        finally
        {
            _lock.Release();
        }
    }

    private async Task<Dictionary<string, string>> ReadAsync(CancellationToken ct)
    {
        if (!File.Exists(_filePath))
            return [];

        var json = await File.ReadAllTextAsync(_filePath, ct);
        if (string.IsNullOrWhiteSpace(json))
            return [];

        return JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? [];
    }

    private async Task WriteAsync(Dictionary<string, string> store, CancellationToken ct)
    {
        var dir = Path.GetDirectoryName(_filePath)!;
        Directory.CreateDirectory(dir);
        var tmp = Path.Combine(dir, Path.GetRandomFileName());
        await File.WriteAllTextAsync(tmp, JsonSerializer.Serialize(store), ct);
        File.Move(tmp, _filePath, overwrite: true);
    }
}
