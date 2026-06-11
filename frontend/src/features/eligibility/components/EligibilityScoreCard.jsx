export default function EligibilityScoreCard({ rules = [] }) {
  const total = rules.length;
  const passed = rules.filter((r) => r.status === 'Passed').length;
  const score = Math.round((passed / (total || 1)) * 100);

  return (
    <div className="rounded-card border p-3">
      <p className="text-sm text-slate-500">Eligibility Score</p>
      <p className="mt-1 text-2xl font-semibold">{score}%</p>
      <div className="mt-3 h-2 w-full rounded bg-slate-100">
        <div style={{ width: `${score}%` }} className="h-2 rounded bg-emerald-600" />
      </div>
      <p className="mt-2 text-xs text-slate-500">{passed} of {total} rules passed</p>
    </div>
  );
}
