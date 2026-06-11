function DriveActivityTimeline({ events = [] }) {
  return (
    <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
      <h2 className="text-lg font-semibold text-slate-950">Drive Activity Timeline</h2>
      <ol className="mt-4 space-y-4">
        {events.map((event) => (
          <li key={event.id} className="border-l-2 border-primary/30 pl-4">
            <p className="text-sm font-semibold text-slate-900">{event.what}</p>
            <p className="mt-1 text-sm text-slate-500">{event.who} · {event.when}</p>
          </li>
        ))}
      </ol>
    </section>
  );
}

export default DriveActivityTimeline;
