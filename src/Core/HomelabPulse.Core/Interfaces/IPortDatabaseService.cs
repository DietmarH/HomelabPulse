namespace HomelabPulse.Core.Interfaces;

public interface IPortDatabaseService
{
    string? GetServiceName(int port, string protocol);
}
