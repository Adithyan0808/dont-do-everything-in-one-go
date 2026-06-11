import { Link } from 'react-router-dom';
import { ShieldAlert } from 'lucide-react';
import { useAuth } from '../hooks/useAuth';

export default function UnauthorizedPage() {
  const { isAuthenticated } = useAuth();

  return (
    <main className="flex min-h-screen items-center justify-center bg-app-bg px-4">
      <section className="w-full max-w-md rounded-card border border-slate-200 bg-card p-8 text-center shadow-md">
        <div className="mx-auto flex h-14 w-14 items-center justify-center rounded-full bg-red-50 text-danger">
          <ShieldAlert className="h-7 w-7" />
        </div>
        <h1 className="mt-5 text-2xl font-semibold text-slate-950">403 Access Denied</h1>
        <p className="mt-2 text-sm text-slate-600">
          Your current role does not have permission to access this area.
        </p>
        <div className="mt-6 flex flex-col gap-3 sm:flex-row sm:justify-center">
          {isAuthenticated && (
            <Link className="rounded-md bg-primary px-4 py-2 text-sm font-semibold text-white" to="/dashboard">
              Return to Dashboard
            </Link>
          )}
          <Link className="rounded-md border border-slate-300 px-4 py-2 text-sm font-semibold text-slate-700" to="/login">
            Return to Login
          </Link>
        </div>
      </section>
    </main>
  );
}
