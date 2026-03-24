using System.Text.Json;
using HomelabPulse.Core.Interfaces;
using HomelabPulse.Core.Models;

namespace HomelabPulse.Persistence;

internal sealed class HostProfileRepository : IHostProfileRepository
{
    private readonly string _filePath;
    private readonly SemaphoreSlim _lock = new(1, 1);

    private static readonly JsonSerializerOptions WriteOptions = new() { WriteIndented = true };

    public HostProfileRepository(string filePath) => _filePath = filePath;

    public async Task<IReadOnlyList<HostProfile>> GetAllAsync(CancellationToken ct = default)
    {
        await _lock.WaitAsync(ct);
        try
        {
            return [.. (await ReadAsync(ct)).Values];
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task<HostProfile?> GetAsync(string id, CancellationToken ct = default)
    {
        await _lock.WaitAsync(ct);
        try
        {
            var map = await ReadAsync(ct);
            return map.TryGetValue(id, out var profile) ? profile : null;
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task SaveAsync(HostProfile profile, CancellationToken ct = default)
    {
        await _lock.WaitAsync(ct);
        try
        {
            var map = await ReadAsync(ct);
            map[profile.Id] = profile;
            await WriteAsync(map, ct);
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task DeleteAsync(string id, CancellationToken ct = default)
    {
        await _lock.WaitAsync(ct);
        try
        {
            var map = await ReadAsync(ct);
            if (map.Remove(id))
                await WriteAsync(map, ct);
        }
        finally
        {
            _lock.Release();
        }
    }

    private async Task<Dictionary<string, HostProfile>> ReadAsync(CancellationToken ct)
    {
        if (!File.Exists(_filePath))
            return [];

        var json = await File.ReadAllTextAsync(_filePath, ct);
        if (string.IsNullOrWhiteSpace(json))
            return [];

        return JsonSerializer.Deserialize<Dictionary<string, HostProfile>>(json) ?? [];
    }

    private async Task WriteAsync(Dictionary<string, HostProfile> map, CancellationToken ct)
    {
        var dir = Path.GetDirectoryName(_filePath)!;
        Directory.CreateDirectory(dir);
        var tmp = Path.Combine(dir, Path.GetRandomFileName());
        await File.WriteAllTextAsync(tmp, JsonSerializer.Serialize(map, WriteOptions), ct);
        File.Move(tmp, _filePath, overwrite: true);
    }
}
