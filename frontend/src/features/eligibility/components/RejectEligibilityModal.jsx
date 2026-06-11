import { useState } from 'react';

const REASONS = [
  'Insufficient Tenure',
  'Training Incomplete',
  'Exceeded Attempts',
  'Budget Unavailable',
  'Policy Restriction',
  'Other',
];

function RejectEligibilityModal({ open, onClose, registration, onConfirm }) {
  const [reason, setReason] = useState(REASONS[0]);
  const [comments, setComments] = useState('');
  if (!open) return null;
  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-slate-950/50 p-4">
      <section className="w-full max-w-lg rounded-card bg-white p-6 shadow-xl">
        <h2 className="text-lg font-semibold">Reject Eligibility</h2>
        <p className="mt-2 text-sm text-slate-600">Registration: {registration?.registrationId}</p>
        <p className="mt-1 text-sm text-slate-600">Candidate: {registration?.candidate?.fullName}</p>

        <div className="mt-4">
          <label className="block text-sm">Reason</label>
          <select value={reason} onChange={(e) => setReason(e.target.value)} className="mt-1 w-full rounded-md border px-3 py-2">
            {REASONS.map((r) => (
              <option key={r} value={r}>{r}</option>
            ))}
          </select>
        </div>

        <div className="mt-3">
          <label className="block text-sm">Comments</label>
          <textarea value={comments} onChange={(e) => setComments(e.target.value)} className="mt-1 w-full rounded-md border px-3 py-2 h-24" />
        </div>

        <div className="mt-4 flex justify-end gap-2">
          <button type="button" className="rounded-md border px-3 py-2" onClick={onClose}>Cancel</button>
          <button
            type="button"
            className="rounded-md bg-red-600 px-3 py-2 text-white"
            onClick={() => {
              onConfirm?.({ reason, comments, approver: 'current.user' });
              onClose?.();
            }}
          >
            Reject
          </button>
        </div>
      </section>
    </div>
  );
}

export default RejectEligibilityModal;
