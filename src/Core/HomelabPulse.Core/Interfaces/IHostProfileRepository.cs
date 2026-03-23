using HomelabPulse.Core.Models;

namespace HomelabPulse.Core.Interfaces;

public interface IHostProfileRepository
{
    Task<IReadOnlyList<HostProfile>> GetAllAsync(CancellationToken ct = default);
    Task<HostProfile?> GetAsync(string id, CancellationToken ct = default);
    Task SaveAsync(HostProfile profile, CancellationToken ct = default);
    Task DeleteAsync(string id, CancellationToken ct = default);
}
