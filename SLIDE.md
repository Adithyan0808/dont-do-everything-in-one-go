# Maverick Certification Hub — Quick Slide

Purpose: Demo-ready full-stack app for presentation.

Demo admin
- Email: admin@maverick.com
- Password: Password123!

Run (local)
- Backend: cd backend && dotnet run --project src/CertificationHub.Api  (http://localhost:5000)
- Frontend: cd frontend && npm ci && npm run dev  (http://localhost:5173)

Key fixes
- Added Manager role and permissions
- Replaced JS-style C# array initializers and initialized collections
- Updated seed data and demo admin password

Quick checks
- GET /health
- Login; view Drives, Registrations, Vouchers

Note: Rotate/remove demo credentials before public release.