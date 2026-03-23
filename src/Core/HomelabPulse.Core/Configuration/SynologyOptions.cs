namespace HomelabPulse.Core.Configuration;

public sealed class SynologyOptions
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = 5001;
    public bool UseSsl { get; set; } = true;
    public string CredentialId { get; set; } = string.Empty;
}
