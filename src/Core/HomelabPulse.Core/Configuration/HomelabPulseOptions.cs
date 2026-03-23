namespace HomelabPulse.Core.Configuration;

public sealed class HomelabPulseOptions
{
    public BackendMode BackendMode { get; set; } = BackendMode.Direct;
    public ApiClientOptions ApiClient { get; set; } = new();
    public SynologyOptions Synology { get; set; } = new();
    public KubernetesOptions Kubernetes { get; set; } = new();
}

public enum BackendMode
{
    Direct,
    Api,
}
