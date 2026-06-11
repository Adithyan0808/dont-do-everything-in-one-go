import { useMemo } from 'react';
import { usePendingApprovals } from '../hooks/useEligibilityQueries';
import ApprovalQueueTable from '../components/ApprovalQueueTable';

export default function ApprovalsPage() {
  const { data: approvals = [], isLoading } = usePendingApprovals();
  const kpis = useMemo(() => ({
    pending: approvals.length,
    highPriority: approvals.filter((a) => a.priority === 'High').length,
  }), [approvals]);

  return (
    <div className="space-y-5">
      <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
        <h1 className="text-2xl font-semibold text-slate-950">Pending Approvals</h1>
        <p className="mt-1 text-sm text-slate-500">Enterprise approval center for eligibility decisions.</p>
        <div className="mt-4 flex gap-4">
          <div className="rounded-card bg-slate-50 p-3">Pending: <strong>{kpis.pending}</strong></div>
          <div className="rounded-card bg-slate-50 p-3">High Priority: <strong>{kpis.highPriority}</strong></div>
        </div>
      </section>

      <section>
        <ApprovalQueueTable approvals={approvals} loading={isLoading} />
      </section>
    </div>
  );
}
