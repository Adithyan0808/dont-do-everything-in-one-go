import { useState } from 'react';
import { Link } from 'react-router-dom';
import { useMyRegistrations } from '../hooks/useRegistrationQueries';
import RegistrationCard from '../components/RegistrationCard';
import RegistrationSkeleton from '../components/RegistrationSkeleton';
import RegistrationErrorState from '../components/RegistrationErrorState';

export default function MyRegistrationsPage() {
  const [view, setView] = useState('card');
  const { data: registrations = [], isLoading, error, refetch } = useMyRegistrations();

  return (
    <div className="space-y-5">
      <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
        <div className="flex items-center justify-between">
          <div>
            <h1 className="text-2xl font-semibold text-slate-950">My Certification Registrations</h1>
            <p className="mt-1 text-sm text-slate-500">Track your certification journey.</p>
          </div>
          <div className="flex items-center gap-2">
            <Link to="/register-drive" className="rounded-md bg-primary px-3 py-2 text-sm font-semibold text-white">Register for Drive</Link>
            <button type="button" className="rounded-md border px-3 py-2 text-sm" onClick={() => refetch()}>Refresh</button>
          </div>
        </div>
        <div className="mt-4 flex gap-2">
          <button type="button" className={`rounded-md px-3 py-2 text-sm ${view === 'card' ? 'bg-primary text-white' : 'bg-slate-100'}`} onClick={() => setView('card')}>Card View</button>
          <button type="button" className={`rounded-md px-3 py-2 text-sm ${view === 'table' ? 'bg-primary text-white' : 'bg-slate-100'}`} onClick={() => setView('table')}>Table View</button>
        </div>
      </section>

      {isLoading && <RegistrationSkeleton />}
      {error && <RegistrationErrorState error={error} onRetry={() => refetch()} />}

      {!isLoading && !error && (
        <section>
          {view === 'card' ? (
            <div className="grid gap-4 lg:grid-cols-2">
              {registrations.map((r) => (
                <RegistrationCard key={r.registrationId} registration={r} />
              ))}
            </div>
          ) : (
            <div className="overflow-auto rounded-card border border-slate-200 bg-white p-4 shadow-sm">
              <table className="w-full table-auto">
                <thead>
                  <tr className="text-left text-sm text-slate-600">
                    <th className="p-2">Registration ID</th>
                    <th className="p-2">Certification</th>
                    <th className="p-2">Vendor</th>
                    <th className="p-2">Status</th>
                    <th className="p-2">Registered Date</th>
                    <th className="p-2">Action</th>
                  </tr>
                </thead>
                <tbody>
                  {registrations.map((r) => (
                    <tr key={r.registrationId} className="border-t">
                      <td className="p-2 text-sm text-slate-700">{r.registrationId}</td>
                      <td className="p-2 text-sm">{r.certificationName}</td>
                      <td className="p-2 text-sm">{r.vendorName}</td>
                      <td className="p-2 text-sm">{r.status}</td>
                      <td className="p-2 text-sm">{r.registeredDate}</td>
                      <td className="p-2 text-sm"><Link to={`/registrations/${r.registrationId}`} className="text-primary underline">View</Link></td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </section>
      )}
    </div>
  );
}
