using HomelabPulse.Core.Interfaces;
using HomelabPulse.Core.Models;
using HomelabPulse.Persistence;
using Xunit;

namespace HomelabPulse.Core.Tests.Persistence;

public sealed class HostProfileRepositoryTests : IDisposable
{
    private readonly string _filePath = Path.GetTempFileName();
    private readonly IHostProfileRepository _sut;

    private static readonly HostProfile ProfileA = new("id-a", "NAS", "192.168.1.10", NodeType.Synology, "cred-a");
    private static readonly HostProfile ProfileB = new("id-b", "k3s", "192.168.1.20", NodeType.Kubernetes, "cred-b");

    public HostProfileRepositoryTests()
    {
        _sut = new HostProfileRepository(_filePath);
    }

    [Fact]
    public async Task GetAllAsync_WhenEmpty_ReturnsEmptyList()
    {
        var result = await _sut.GetAllAsync();

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAsync_WhenNotFound_ReturnsNull()
    {
        var result = await _sut.GetAsync("missing");

        Assert.Null(result);
    }

    [Fact]
    public async Task SaveAsync_ThenGetAsync_ReturnsProfile()
    {
        await _sut.SaveAsync(ProfileA);

        var result = await _sut.GetAsync(ProfileA.Id);

        Assert.Equal(ProfileA, result);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllSavedProfiles()
    {
        await _sut.SaveAsync(ProfileA);
        await _sut.SaveAsync(ProfileB);

        var result = await _sut.GetAllAsync();

        Assert.Equal(2, result.Count);
        Assert.Contains(ProfileA, result);
        Assert.Contains(ProfileB, result);
    }

    [Fact]
    public async Task SaveAsync_OverwritesExistingProfile()
    {
        await _sut.SaveAsync(ProfileA);
        var updated = ProfileA with { DisplayName = "Updated NAS" };

        await _sut.SaveAsync(updated);

        var result = await _sut.GetAsync(ProfileA.Id);
        Assert.Equal("Updated NAS", result!.DisplayName);
    }

    [Fact]
    public async Task DeleteAsync_WhenExists_RemovesProfile()
    {
        await _sut.SaveAsync(ProfileA);

        await _sut.DeleteAsync(ProfileA.Id);

        Assert.Null(await _sut.GetAsync(ProfileA.Id));
    }

    [Fact]
    public async Task DeleteAsync_WhenNotFound_DoesNotThrow()
    {
        await _sut.DeleteAsync("nonexistent");
    }

    [Fact]
    public async Task SaveAsync_PersistsToDisk_SurvivesNewInstance()
    {
        await _sut.SaveAsync(ProfileA);

        var second = new HostProfileRepository(_filePath);
        var result = await second.GetAsync(ProfileA.Id);

        Assert.Equal(ProfileA, result);
    }

    public void Dispose()
    {
        if (File.Exists(_filePath))
            File.Delete(_filePath);
    }
}
