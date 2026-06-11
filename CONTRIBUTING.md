# Contributing

Thanks for contributing! Please follow these guidelines.

Branching
- Create feature branches: `feature/<short-desc>` or `fix/<short-desc>`

Local setup
- Backend: `dotnet restore && dotnet build`
- Run tests: `dotnet test`
- Frontend: `cd frontend && npm ci && npm run build`

Commits & PRs
- Use conventional commit style: `type(scope): short description`
- Open a PR against `master` with description, checklist, and links to issues
- Ensure CI passes before requesting final review

Code review
- Add reviewers and respect CODEOWNERS
- Address review comments and keep PR small and focused
