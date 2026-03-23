namespace HomelabPulse.Core.Configuration;

public sealed class KubernetesOptions
{
    public string KubeconfigPath { get; set; } = string.Empty;
    public string? Context { get; set; }
    public string SshCredentialId { get; set; } = string.Empty;
}
