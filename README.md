# HomelabPulse

[![CI](https://github.com/DietmarH/HomelabPulse/actions/workflows/ci.yml/badge.svg)](https://github.com/DietmarH/HomelabPulse/actions/workflows/ci.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple)](https://dotnet.microsoft.com/)

**HomelabPulse** is a C# Avalonia desktop application that provides centralized network visibility across your home lab. Instead of blind port scanning, it uses authenticated API and protocol-level interrogation to show you exactly what is running — and why — across your servers and clusters.

> **Current scope:** Synology NAS and k3s clusters. Additional platforms (Proxmox, Windows, Linux) are planned for future milestones.

---

## Table of Contents

- [Why HomelabPulse?](#why-homelabpulse)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [Security](#security)
- [License](#license)

---

## Why HomelabPulse?

As home labs grow, services become distributed across physical hardware, virtual machines, and containerized environments. This creates challenges that traditional tools do not solve well:

| Problem | How HomelabPulse addresses it |
|---|---|
| **Visibility gap** — port scanners show an open port but not what is behind it, especially behind container NAT | Queries management APIs directly to retrieve internal service state |
| **Information silos** — each platform has its own web UI and CLI | Consolidates Synology DSM, k3s, and more into a single interface |
| **Network noise** — frequent blind scans can trigger security alerts or saturate low-power devices | Authenticated, targeted interrogation replaces broad scanning |

HomelabPulse acts as the heartbeat of your lab: transforming raw network data into actionable service maps.

---

## Features

### Synology NAS

- Authenticated queries against the Synology DSM Web API
- Targets `SYNO.Core.Service` and `SYNO.Core.Network` namespaces
- Maps active services (e.g., Hyper Backup Vault, Surveillance Station) to their network ports

### k3s Clusters

- Cluster-level service discovery via the Kubernetes .NET client
- Maps host ports to internal container ports to eliminate the guesswork of NodePort and LoadBalancer services
- SSH-based socket statistics (`ss -lntu`) to correlate PIDs with listening sockets on cluster nodes

### Core

- **Responsive UI** — singleton-based asynchronous management pipeline scans multiple nodes simultaneously without blocking the interface
- **Secure credential storage** — server credentials and host profiles are encrypted at rest
- **Service name database** — maps common ports to descriptive names (e.g., port 5001 → "Synology DSM HTTPS")

---

## Technology Stack

| Component | Technology |
|---|---|
| Language | C# |
| Framework | .NET 10 |
| Desktop UI | [Avalonia UI](https://avaloniaui.net/) — cross-platform (Windows, Linux, macOS) |
| MVVM | [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet) |
| Optional API server | ASP.NET Core minimal API |
| Kubernetes integration | [Kubernetes client for .NET](https://github.com/kubernetes-client/csharp) |
| SSH interrogation | [SSH.NET](https://github.com/sshnet/SSH.NET) |

---

## Prerequisites

- [.NET SDK 10.0+](https://dotnet.microsoft.com/download)
- Git
- Windows, Linux, or macOS (Avalonia is cross-platform)

For the services you intend to monitor:
- **Synology NAS** — DSM 7.x with API access enabled and a dedicated account with appropriate read permissions
- **k3s cluster** — a valid `kubeconfig` with read access, and SSH access to cluster nodes if socket-level interrogation is needed

---

## Getting Started

> The project is in early development. These steps will be updated as project files are added.

```bash
git clone https://github.com/your-github-username/HomelabPulse.git
cd HomelabPulse
dotnet restore
dotnet build
dotnet test
```

To run the application:

```bash
dotnet run --project src/UI/HomelabPulse.Desktop
```

---

## Project Structure

```
HomelabPulse/
├── src/
│   ├── Core/
│   │   └── HomelabPulse.Core/                    # Domain models, interfaces, service contracts
│   ├── Infrastructure/
│   │   ├── HomelabPulse.Integrations.Synology/   # Synology DSM API integration
│   │   ├── HomelabPulse.Integrations.Kubernetes/ # k3s cluster + SSH socket probing
│   │   └── HomelabPulse.Persistence/             # Credentials, host profiles, port database
│   ├── Services/
│   │   └── HomelabPulse.Api/                     # Optional ASP.NET Core server (remote mode)
│   └── UI/
│       ├── HomelabPulse.ApiClient/               # HTTP client implementing Core interfaces
│       ├── HomelabPulse.Desktop/                 # Avalonia desktop app (direct or API mode)
│       └── HomelabPulse.Blazor/                  # Web app — future milestone
├── tests/
│   ├── HomelabPulse.Core.Tests/
│   ├── HomelabPulse.Integrations.Tests/
│   └── HomelabPulse.Api.IntegrationTests/
├── Directory.Build.props                         # Shared MSBuild properties
├── Directory.Packages.props                      # Central NuGet package versions
└── HomelabPulse.slnx
```

---

## Roadmap

### Phase 0 — Foundation ✅

| Milestone | Description |
|---|---|
| **0.1** — Persistence layer | `CredentialStore`, `HostProfileRepository`, `PortDatabaseService`, DI registration |

### Phase 1 — Direct Core Integration

The Avalonia desktop app runs all integrations in-process (`BackendMode.Direct`).

| Milestone | Description |
|---|---|
| **1.1** — Synology DSM system services | Authenticated DSM API; first live data in the port table |
| **1.2** — Universal container interrogation | Synology Container Manager + Kubernetes/k3s via SSH socket stats |
| **1.3** — Synology advanced network & macvlan | Surface macvlan interfaces invisible to host-side port scans |
| **1.4** — k3s cluster-wide discovery | Map every internal k3s service to its external access point |

### Phase 2 — Expanding the Ecosystem

| Milestone | Description |
|---|---|
| **2.1** — `HomelabPulse.Api` | ASP.NET Core Minimal API exposing nodes, scan, credentials, and profiles over HTTP |
| **2.2** — `HomelabPulse.ApiClient` | HTTP implementations of Core interfaces enabling `BackendMode.Api` with no UI changes |
| **2.3** — Blazor web dashboard | Browser-based view of lab status consuming the API; real-time updates via polling or SignalR |

See [open issues](https://github.com/DietmarH/HomelabPulse/issues) for planned work and known bugs.

---

## Contributing

Contributions are welcome. Please read [CONTRIBUTING.md](CONTRIBUTING.md) before submitting a pull request.

The short version:
1. Fork the repository and create a feature branch
2. Make your changes and run `dotnet format` before pushing
3. Open a pull request — CI must be green

---

## Security

Please do not report security vulnerabilities through public GitHub issues. See [SECURITY.md](SECURITY.md) for the responsible disclosure process.

---

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.
