# Contributing

Thanks for your interest in contributing.

## Workflow Requirements

All changes must follow this workflow:

1. Start with a GitHub issue that defines the change.
2. Create a branch from `main` for that issue.
3. Name the branch as `feature/<issue-number>-<slug>`, `fix/<issue-number>-<slug>`, `docs/<issue-number>-<slug>`, or `chore/<issue-number>-<slug>`.
4. Do all work on that branch. Never commit directly to `main`.
5. Open a pull request with a body that contains `Closes #<issue-number>`.
6. Wait for CI to pass before merging.
7. Perform a review before merge, even for solo work.
8. Squash merge into `main`.
9. Delete the branch after merge.

`main` must stay releasable. Do not merge incomplete work or work with failing checks.

## Development Setup

1. Install .NET SDK 10.0+
2. Fork and clone the repository
3. Create an issue-linked branch from `main`
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
- Reference the linked issue in the PR body with `Closes #<issue-number>`
- Add or update tests when behavior changes
- Update documentation if needed
- Ensure CI is green
- Complete a review before merge
- Use squash merge
- Delete the branch after merge

## Releases

Release tags are created manually on `main` using semantic version tags such as `v0.1.0`.

MinVer reads tags with the `v` prefix, so only create release tags on `main` after the branch has been squash-merged and CI is green.

## Commit Messages

Use clear, descriptive commit messages.
