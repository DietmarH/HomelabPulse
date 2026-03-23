# Contributing

Thanks for your interest in contributing.

## Development Setup

1. Install .NET SDK 10.0+
2. Fork and clone the repository
3. Create a feature branch
4. Implement your changes
5. Run local checks:

```bash
dotnet restore
dotnet build
dotnet test
dotnet format
```

> Run `dotnet format` before pushing — CI enforces formatting via `--verify-no-changes` and will fail if the code is not formatted.

## Pull Requests

- Keep PRs focused and small
- Add or update tests when behavior changes
- Update documentation if needed
- Ensure CI is green

## Commit Messages

Use clear, descriptive commit messages.
