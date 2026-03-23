namespace HomelabPulse.Core.Models;

public sealed record ScanResult(
    string NodeId,
    DateTimeOffset ScannedAt,
    IReadOnlyList<PortBinding> Bindings,
    bool IsSuccess,
    string? ErrorMessage = null);
