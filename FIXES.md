Fixes applied — 2026-06-11

## Summary

Fixed frontend/back-end inconsistencies and C# syntax issues that caused runtime/build failures. Rebuilt frontend and ran backend tests to verify.

## Frontend changes

- frontend/src/features/auth/utils/permissions.js
  - Added AUTH_ROLES.manager = 'Manager'.
  - Added Manager entry in rolePermissions (same permissions as Approver).
  - Rationale: Some route/feature checks referenced "Manager"; missing mapping caused role/permission checks to behave incorrectly.

## Backend changes

- CertificationHub.Api/Models/ApiResponse.cs
  - Replaced invalid C# initializer ([]) with Array.Empty<string>() for Errors.

- CertificationHub.Domain/Common/BaseEntity.cs
  - RowVersion = Array.Empty<byte>();
  - \_domainEvents = new List<DomainEvent>();

- CertificationHub.Domain/Entities/User.cs
- CertificationHub.Domain/Entities/CertificationDrive.cs
  - Initialized collection properties using new List<T>() instead of [] to avoid invalid C# syntax and runtime null/empty issues.

- CertificationHub.Api/Extensions/ApiServiceRegistration.cs
  - Fixed OpenAPI security requirement array to use Array.Empty<string>().
  - Fixed TagActionsBy to return new[] { ... } instead of JS-like [ ... ].

- CertificationHub.Api/Controllers/AssessmentsController.cs
  - AllowedExtensions declared as new[] { ".csv", ".xlsx", ".xls" }.
  - BadRequest error arrays passed as new[] { "..." }.

- CertificationHub.Api/Controllers/AuthController.cs
  - Unauthorized ApiResponse.Fail call uses new[] for error messages.

- CertificationHub.Api/Controllers/EligibilityController.cs
  - Constructed EligibilityResultDto with Array.Empty and new[] arguments (no JS-like arrays).

## Verification

- Backend: dotnet build -c Release succeeded.
- Backend tests: dotnet test -c Release => Test summary: total: 2, failed: 0, succeeded: 2.
- Frontend: removed locked native binding and node_modules, ran npm ci and npm run build; frontend/dist/index.html produced.

## Commands executed

- Backend: dotnet build -c Release
- Backend tests: dotnet test -c Release
- Frontend (fix): Set-ExecutionPolicy RemoteSigned (current user), removed node_modules, npm cache clean --force, npm ci, npm run build

## Notes

- Changes are local. This repository is not yet under git control.
- If desired, a local git repository can be initialized and a commit created with a message describing these fixes. Commits will include the required Co-authored-by trailer.

If you want the assistant to initialize git and commit these changes, approve when prompted.
