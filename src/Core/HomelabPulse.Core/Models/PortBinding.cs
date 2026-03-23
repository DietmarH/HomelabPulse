namespace HomelabPulse.Core.Models;

public sealed record PortBinding(
    int Port,
    string Protocol,
    string? ServiceName,
    string? ProcessName,
    int? Pid);
