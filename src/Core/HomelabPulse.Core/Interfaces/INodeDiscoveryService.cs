using HomelabPulse.Core.Models;

namespace HomelabPulse.Core.Interfaces;

public interface INodeDiscoveryService
{
    Task<IReadOnlyList<Node>> DiscoverNodesAsync(CancellationToken ct = default);
    Task<Node?> GetNodeAsync(string nodeId, CancellationToken ct = default);
}
