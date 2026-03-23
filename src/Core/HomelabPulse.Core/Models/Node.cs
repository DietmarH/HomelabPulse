namespace HomelabPulse.Core.Models;

public sealed record Node(
    string Id,
    string DisplayName,
    string Host,
    NodeType Type);
