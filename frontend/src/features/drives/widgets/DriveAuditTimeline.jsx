import { mockDriveAudit } from '../services/driveMockData';

function DriveAuditTimeline({ events = mockDriveAudit }) {
  return (
    <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
      <h2 className="text-lg font-semibold text-slate-950">Audit Trail</h2>
      <div className="mt-4 space-y-4">
        {events.map((event) => (
          <article key={event.id} className="rounded-card border border-slate-200 p-4">
            <p className="font-semibold text-slate-900">{event.what}</p>
            <p className="mt-1 text-sm text-slate-500">{event.who} · {event.when}</p>
            <div className="mt-3 grid gap-3 text-xs lg:grid-cols-2">
              <pre className="overflow-auto rounded bg-slate-50 p-3">Before: {event.before}</pre>
              <pre className="overflow-auto rounded bg-slate-50 p-3">After: {event.after}</pre>
            </div>
          </article>
        ))}
      </div>
    </section>
  );
}

export default DriveAuditTimeline;
