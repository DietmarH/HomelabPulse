namespace HomelabPulse.Persistence;

public sealed class PersistenceOptions
{
    public string DataDirectory { get; set; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "HomelabPulse");
}
