import { Link } from 'react-router-dom';
import { useApproveEligibility, useRejectEligibility } from '../hooks/useEligibilityQueries';

export default function ApprovalQueueTable({ approvals = [], loading }) {
  const approve = useApproveEligibility();
  const reject = useRejectEligibility();

  const handleApprove = (id) => {
    if (!confirm(`Approve ${id}?`)) return;
    approve.mutate({ registrationId: id, payload: { approver: 'current.user' } });
  };

  const handleReject = (id) => {
    const reason = prompt('Enter rejection reason (short):', 'Insufficient Tenure');
    if (!reason) return;
    reject.mutate({ registrationId: id, payload: { approver: 'current.user', reason } });
  };

  if (loading) return <div className="rounded-card p-4">Loading approvals…</div>;

  if (!approvals.length) return <div className="rounded-card p-4">No pending approvals.</div>;

  return (
    <div className="overflow-auto rounded-card border border-slate-200 bg-white p-4 shadow-sm">
      <table className="w-full table-auto">
        <thead>
          <tr className="text-left text-sm text-slate-600">
            <th className="p-2">Candidate</th>
            <th className="p-2">Employee ID</th>
            <th className="p-2">Certification</th>
            <th className="p-2">Drive</th>
            <th className="p-2">Submitted</th>
            <th className="p-2">Eligibility</th>
            <th className="p-2">Priority</th>
            <th className="p-2">Action</th>
          </tr>
        </thead>
        <tbody>
          {approvals.map((a) => (
            <tr key={a.registrationId} className="border-t">
              <td className="p-2 text-sm">{a.candidate}</td>
              <td className="p-2 text-sm">{a.employeeId}</td>
              <td className="p-2 text-sm">{a.certification}</td>
              <td className="p-2 text-sm">{a.drive}</td>
              <td className="p-2 text-sm">{a.submittedDate}</td>
              <td className="p-2 text-sm">{a.eligibility}</td>
              <td className="p-2 text-sm">{a.priority}</td>
              <td className="p-2 text-sm">
                <div className="flex gap-2">
                  <button type="button" className="rounded-md bg-emerald-600 px-2 py-1 text-white text-sm" onClick={() => handleApprove(a.registrationId)}>Approve</button>
                  <button type="button" className="rounded-md bg-red-600 px-2 py-1 text-white text-sm" onClick={() => handleReject(a.registrationId)}>Reject</button>
                  <Link to={`/registrations/${a.registrationId}`} className="text-primary underline">View</Link>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
