using HomelabPulse.Core.Interfaces;
using HomelabPulse.Persistence;
using Xunit;

namespace HomelabPulse.Core.Tests.Persistence;

public sealed class PortDatabaseServiceTests
{
    private readonly IPortDatabaseService _sut = new PortDatabaseService();

    [Theory]
    [InlineData(22, "tcp", "ssh")]
    [InlineData(80, "tcp", "http")]
    [InlineData(443, "tcp", "https")]
    [InlineData(5432, "tcp", "postgresql")]
    [InlineData(6379, "tcp", "redis")]
    [InlineData(6443, "tcp", "kubernetes-api")]
    [InlineData(53, "udp", "dns")]
    [InlineData(123, "udp", "ntp")]
    [InlineData(51820, "udp", "wireguard")]
    public void GetServiceName_KnownPort_ReturnsExpectedName(int port, string protocol, string expected)
    {
        var result = _sut.GetServiceName(port, protocol);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetServiceName_UnknownPort_ReturnsNull()
    {
        var result = _sut.GetServiceName(65000, "tcp");

        Assert.Null(result);
    }

    [Fact]
    public void GetServiceName_ProtocolIsCaseInsensitive()
    {
        var lower = _sut.GetServiceName(22, "tcp");
        var upper = _sut.GetServiceName(22, "TCP");
        var mixed = _sut.GetServiceName(22, "Tcp");

        Assert.Equal(lower, upper);
        Assert.Equal(lower, mixed);
    }

    [Fact]
    public void GetServiceName_WrongProtocol_ReturnsNull()
    {
        var result = _sut.GetServiceName(80, "udp");

        Assert.Null(result);
    }
}
