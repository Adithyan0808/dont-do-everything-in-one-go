export default function RuleResultBadge({ status }) {
  const map = {
    Passed: 'bg-emerald-100 text-emerald-700',
    Warning: 'bg-amber-100 text-amber-700',
    Failed: 'bg-red-100 text-red-700',
    Skipped: 'bg-slate-100 text-slate-700',
  };
  const cls = map[status] || map.Skipped;
  return <span className={`inline-flex rounded-full px-2 py-0.5 text-xs font-semibold ${cls}`}>{status}</span>;
}
