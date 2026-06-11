# Maverick Certification Hub

Full-stack certification drive automation platform scaffold.

## Tech Stack

- Backend: .NET 8 Web API with Clean Architecture
- Frontend: React + Vite with Tailwind CSS
- Database: SQLite
- Auth: JWT Authentication
- API Communication: Axios

## Run Backend

```powershell
cd backend
dotnet run --project src/CertificationHub.Api
```

The API is configured for `http://localhost:5000`.

## Run Frontend

```powershell
cd frontend
npm install
npm run dev
```

The React dev server is configured for `http://localhost:5173` with `/api` proxied to the backend.
