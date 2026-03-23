namespace HomelabPulse.Core.Models;

public sealed record HostProfile(
    string Id,
    string DisplayName,
    string Host,
    NodeType Type,
    string CredentialId);
