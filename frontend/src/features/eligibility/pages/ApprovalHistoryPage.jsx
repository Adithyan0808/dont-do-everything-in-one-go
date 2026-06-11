import { useApprovalHistory } from '../hooks/useEligibilityQueries';

export default function ApprovalHistoryPage({ registrationId }) {
  const { data: history = [], isLoading } = useApprovalHistory(registrationId);
  if (isLoading) return <div className="rounded-card p-4">Loading approval history...</div>;
  if (!history.length) return <div className="rounded-card p-4">No approval history found.</div>;

  return (
    <div className="rounded-card border p-4">
      <h2 className="text-lg font-semibold">Approval History</h2>
      <div className="mt-3 overflow-auto">
        <table className="w-full table-auto">
          <thead>
            <tr className="text-left text-sm text-slate-600">
              <th className="p-2">Date</th>
              <th className="p-2">Registration</th>
              <th className="p-2">Approver</th>
              <th className="p-2">Decision</th>
              <th className="p-2">Reason</th>
            </tr>
          </thead>
          <tbody>
            {history.map((h) => (
              <tr key={h.id} className="border-t">
                <td className="p-2 text-sm">{new Date(h.timestamp).toLocaleString()}</td>
                <td className="p-2 text-sm">{h.registrationId}</td>
                <td className="p-2 text-sm">{h.approver}</td>
                <td className="p-2 text-sm">{h.decision}</td>
                <td className="p-2 text-sm">{h.reason}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
