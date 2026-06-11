import { useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import RegistrationStatusBadge from '../components/RegistrationStatusBadge';
import RegistrationJourneyTracker from '../components/RegistrationJourneyTracker';
import RegistrationErrorState from '../components/RegistrationErrorState';
import DetailWorkspaceSkeleton from '../components/DetailWorkspaceSkeleton';
import { useRegistration, useRegistrationEligibility, useRegistrationAudit, useRegistrationCommunications } from '../hooks/useRegistrationQueries';

export default function RegistrationDetailsPage() {
  const { registrationId } = useParams();
  const { data: registration, isLoading, error, refetch } = useRegistration(registrationId);
  const { data: eligibility, isLoading: eligLoading } = useRegistrationEligibility(registrationId);
  const { data: audit, isLoading: auditLoading } = useRegistrationAudit(registrationId);
  const { data: comms, isLoading: commsLoading } = useRegistrationCommunications(registrationId);
  const [tab, setTab] = useState('overview');

  if (isLoading) return <DetailWorkspaceSkeleton />;
  if (error) return <RegistrationErrorState error={error} onRetry={() => refetch()} />;

  return (
    <div className="space-y-5">
      <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
        <div className="flex items-start justify-between">
          <div>
            <h1 className="text-2xl font-semibold text-slate-950">{registration.certificationName}</h1>
            <div className="mt-2 flex items-center gap-2">
              <RegistrationStatusBadge status={registration.status} />
              <span className="text-sm text-slate-500">{registration.registrationId}</span>
            </div>
            <p className="mt-2 text-sm text-slate-500">{registration.vendorName} · {registration.driveName}</p>
            <p className="mt-1 text-sm text-slate-500">Candidate: {registration.candidate?.fullName} · {registration.candidate?.employeeId}</p>
          </div>
          <div className="flex items-center gap-2">
            <Link to="/my-registrations" className="rounded-md border px-3 py-2 text-sm">Back</Link>
            <button type="button" className="rounded-md bg-primary px-3 py-2 text-sm font-semibold text-white" onClick={() => refetch()}>Refresh</button>
          </div>
        </div>

        <div className="mt-4">
          <RegistrationJourneyTracker status={registration.status} />
        </div>
      </section>

      <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
        <div className="flex flex-wrap gap-2">
          {['overview', 'eligibility', 'approvals', 'assessment', 'voucher', 'communications', 'audit'].map((t) => (
            <button key={t} type="button" className={`rounded-full px-3 py-1.5 text-sm ${tab === t ? 'bg-primary text-white' : 'bg-slate-100 text-slate-600'}`} onClick={() => setTab(t)}>
              {t.charAt(0).toUpperCase() + t.slice(1)}
            </button>
          ))}
        </div>

        <div className="mt-4">
          {tab === 'overview' && (
            <div className="grid gap-4 lg:grid-cols-2">
              <article className="rounded-card border border-slate-200 p-4">
                <h3 className="font-semibold text-slate-900">Registration Metadata</h3>
                <p className="mt-2 text-sm text-slate-600">Registered: {registration.registeredDate}</p>
                <p className="text-sm text-slate-600">Status: {registration.status}</p>
                <p className="text-sm text-slate-600">Next Exam: {registration.nextExamDate || 'N/A'}</p>
              </article>

              <article className="rounded-card border border-slate-200 p-4">
                <h3 className="font-semibold text-slate-900">Candidate</h3>
                <p className="mt-2 text-sm text-slate-600">{registration.candidate?.fullName}</p>
                <p className="text-sm text-slate-600">{registration.candidate?.employeeId} · {registration.candidate?.email}</p>
                <p className="text-sm text-slate-600">{registration.candidate?.department} · {registration.candidate?.location}</p>
              </article>
            </div>
          )}

          {tab === 'eligibility' && (eligLoading ? <DetailWorkspaceSkeleton /> : (
            <div>
              <h3 className="font-semibold text-slate-900">Eligibility Rules</h3>
              <div className="mt-3 space-y-2">
                {eligibility?.map((r) => (
                  <div key={r.ruleName} className="rounded-card border p-3">
                    <p className="font-medium">{r.ruleName} — <span className="font-semibold">{r.status}</span></p>
                    <p className="text-sm text-slate-600">{r.reason}</p>
                  </div>
                ))}
              </div>
            </div>
          ))}

          {tab === 'audit' && (auditLoading ? <DetailWorkspaceSkeleton /> : (
            <div>
              <h3 className="font-semibold text-slate-900">Audit Trail</h3>
              <div className="mt-3 space-y-2">
                {audit?.map((a) => (
                  <div key={a.id} className="rounded-card border p-3">
                    <p className="font-medium">{a.actor} — <span className="text-sm text-slate-500">{a.timestamp}</span></p>
                    <p className="text-sm text-slate-600">{a.action}</p>
                    <pre className="mt-2 text-xs font-mono text-slate-500">{a.before} → {a.after}</pre>
                  </div>
                ))}
              </div>
            </div>
          ))}

          {tab === 'communications' && (commsLoading ? <DetailWorkspaceSkeleton /> : (
            <div>
              <h3 className="font-semibold text-slate-900">Communications</h3>
              <div className="mt-3 space-y-2">
                {comms?.map((c) => (
                  <div key={c.id} className="rounded-card border p-3">
                    <p className="font-medium">{c.channel} — <span className="text-sm text-slate-500">{c.timestamp}</span></p>
                    <p className="text-sm text-slate-600">{c.subject} — {c.status}</p>
                  </div>
                ))}
              </div>
            </div>
          ))}

          {tab === 'approvals' && <div className="text-sm text-slate-600">Approvals tab (summary) — use approvals workbench for bulk actions.</div>}
          {tab === 'assessment' && <div className="text-sm text-slate-600">Assessment tab (submit/attempts)</div>}
          {tab === 'voucher' && <div className="text-sm text-slate-600">Voucher information and redemption history.</div>}
        </div>
      </section>
    </div>
  );
}
