using HomelabPulse.Core.Models;

namespace HomelabPulse.Core.Interfaces;

public interface IScanOrchestrator
{
    Task<ScanResult> ScanNodeAsync(string nodeId, CancellationToken ct = default);
    Task<IReadOnlyList<ScanResult>> ScanAllAsync(CancellationToken ct = default);
}
