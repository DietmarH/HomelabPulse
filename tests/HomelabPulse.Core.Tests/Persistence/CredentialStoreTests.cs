using HomelabPulse.Core.Interfaces;
using HomelabPulse.Persistence;
using Microsoft.AspNetCore.DataProtection;
using NSubstitute;
using Xunit;

namespace HomelabPulse.Core.Tests.Persistence;

public sealed class CredentialStoreTests : IDisposable
{
    private readonly string _filePath = Path.GetTempFileName();
    private readonly IDataProtectionProvider _protection;
    private readonly ICredentialStore _sut;

    public CredentialStoreTests()
    {
        var protector = Substitute.For<IDataProtector>();
        protector.CreateProtector(Arg.Any<string>()).Returns(protector);
        protector.Protect(Arg.Any<byte[]>()).Returns(x => x.ArgAt<byte[]>(0));
        protector.Unprotect(Arg.Any<byte[]>()).Returns(x => x.ArgAt<byte[]>(0));

        _protection = Substitute.For<IDataProtectionProvider>();
        _protection.CreateProtector(Arg.Any<string>()).Returns(protector);

        _sut = new CredentialStore(_protection, _filePath);
    }

    [Fact]
    public async Task GetAsync_WhenCredentialNotFound_ReturnsNull()
    {
        var result = await _sut.GetAsync("missing");

        Assert.Null(result);
    }

    [Fact]
    public async Task SetAsync_ThenGetAsync_ReturnsSecret()
    {
        await _sut.SetAsync("id-1", "s3cr3t");

        var result = await _sut.GetAsync("id-1");

        Assert.Equal("s3cr3t", result);
    }

    [Fact]
    public async Task SetAsync_OverwritesExistingCredential()
    {
        await _sut.SetAsync("id-1", "old");
        await _sut.SetAsync("id-1", "new");

        var result = await _sut.GetAsync("id-1");

        Assert.Equal("new", result);
    }

    [Fact]
    public async Task DeleteAsync_WhenExists_RemovesCredential()
    {
        await _sut.SetAsync("id-1", "s3cr3t");

        await _sut.DeleteAsync("id-1");

        Assert.Null(await _sut.GetAsync("id-1"));
    }

    [Fact]
    public async Task DeleteAsync_WhenNotFound_DoesNotThrow()
    {
        await _sut.DeleteAsync("nonexistent");
    }

    [Fact]
    public async Task SetAsync_PersistsToDisk_SurvivesNewInstance()
    {
        await _sut.SetAsync("id-1", "s3cr3t");

        var second = new CredentialStore(_protection, _filePath);
        var result = await second.GetAsync("id-1");

        Assert.Equal("s3cr3t", result);
    }

    public void Dispose()
    {
        if (File.Exists(_filePath))
            File.Delete(_filePath);
    }
}
