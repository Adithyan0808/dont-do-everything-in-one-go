import { useState } from 'react';

export default function EligibilityRuleCard({ rule }) {
  const [open, setOpen] = useState(false);
  return (
    <article className="rounded-card border p-3">
      <div className="flex items-start justify-between">
        <div>
          <p className="font-medium">{rule.ruleName} <span className="ml-2 text-xs text-slate-500">({rule.severity})</span></p>
          <p className="mt-1 text-sm text-slate-600">Status: <strong>{rule.status}</strong></p>
        </div>
        <div className="flex items-center gap-2">
          <button type="button" className="text-sm rounded-md border px-2 py-1" onClick={() => setOpen((s) => !s)}>{open ? 'Collapse' : 'Details'}</button>
        </div>
      </div>

      {open && (
        <div className="mt-3 border-t pt-3">
          <p className="text-sm text-slate-700">Reason: {rule.reason}</p>
          <p className="mt-2 text-xs text-slate-500">Evidence: {rule.evidence}</p>
          <p className="mt-2 text-xs text-slate-400">Evaluated: {rule.timestamp}</p>
        </div>
      )}
    </article>
  );
}
