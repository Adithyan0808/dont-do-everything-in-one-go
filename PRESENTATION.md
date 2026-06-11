PRESENTATION GUIDE — Maverick Certification Hub

Purpose
-------
Make the project presentation-ready: runnable locally with seeded demo data and an admin login, and provide step-by-step instructions, verification steps, and a change log of fixes applied.

Demo admin credentials (for presentation)
--------------------------------------
- Email: admin@maverick.com
- Password: Password123!

Summary of what was changed
---------------------------
1. Frontend
   - Added Manager role mapping and permissions.
   - Files: frontend/src/features/auth/utils/permissions.js
2. Backend (C# fixes)
   - Replaced JavaScript-style array initializers ([]) with valid C# initializers (Array.Empty<T>() or new List<T>()).
   - Fixed OpenAPI security scopes and TagActionsBy usage.
   - Fixed controller error-response array arguments.
   - Files (key list):
     - backend/src/CertificationHub.Api/Models/ApiResponse.cs
     - backend/src/CertificationHub.Domain/Common/BaseEntity.cs
     - backend/src/CertificationHub.Domain/Entities/User.cs
     - backend/src/CertificationHub.Domain/Entities/CertificationDrive.cs
     - backend/src/CertificationHub.Api/Extensions/ApiServiceRegistration.cs
     - backend/src/CertificationHub.Api/Controllers/AssessmentsController.cs
     - backend/src/CertificationHub.Api/Controllers/AuthController.cs
     - backend/src/CertificationHub.Api/Controllers/EligibilityController.cs
3. Seed data
   - Set demo admin password hash to a bcrypt value for password: Password123!
   - File: backend/src/CertificationHub.Infrastructure/Persistence/Seed/ApplicationModelSeeder.cs
4. Devops
   - Initialized local git, pushed to GitHub: https://github.com/Adithyan0808/dont-do-everything-in-one-go
   - Added CI workflow: .github/workflows/ci.yml (builds .NET, runs tests, builds frontend)
   - Added governance: CONTRIBUTING.md, PR/Issue templates, CODEOWNERS, Dependabot, SECURITY.md
5. Docs
   - FIXES.md and this PRESENTATION.md created in repo root.

How to run locally for presentation
----------------------------------
Prerequisites:
- .NET 8 SDK
- Node.js 20+ and npm
- Optional: Yarn

1. Start backend (API + DB seed)
   - Open terminal, go to repo root
   - cd backend
   - dotnet run --project src/CertificationHub.Api
   - By default the API listens on http://localhost:5000 (see README)
   - The EF Core seed will create demo data including admin@maverick.com (Password123!).

2. Start frontend
   - In a new terminal: cd frontend
   - npm ci
   - npm run dev
   - Open http://localhost:5173

3. Login and demo flow
   - Visit /login and sign in with admin@maverick.com / Password123!
   - Dashboard: navigate to Drives, Registrations, Vouchers, Reports to show seeded data
   - Example flows:
     - View Drive details -> see seeded AWS SAA drive
     - Open Registrations -> see approved/pending examples
     - Voucher pool -> view masked codes and assigned vouchers

Verification checklist (quick)
------------------------------
- [ ] Backend starts without errors and listens on port 5000
- [ ] Frontend dev server starts on 5173
- [ ] Login succeeds using admin@maverick.com / Password123!
- [ ] Dashboard shows seeded drives and registrations
- [ ] API health: GET http://localhost:5000/health

Troubleshooting
---------------
- PowerShell npm script policy: run Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy RemoteSigned
- If frontend fails due to locked native binding: stop node processes, delete node_modules, run npm ci
- If login fails: ensure backend was started after clearing any existing local DB so seed can apply. Delete local SQLite DB (if present) and restart backend to reseed.

Files changed (commit reference)
--------------------------------
See recent commits in the repo; main commit that included fixes is the most recent on master.

Presentation checklist (recommended order)
-----------------------------------------
1. Start backend (dotnet run)
2. Confirm health endpoint
3. Start frontend (npm run dev)
4. Open browser to http://localhost:5173 and sign in
5. Walk through dashboard & sample data

Contact
-------
If anything fails during the run, report the failure with logs and I'll help debug.

Notes
-----
- The seeded admin password was set for demo use only. Remove or rotate before publishing the repository publicly if needed.
- The CI runs builds and tests on push/PR; enabling branch protection is recommended.
