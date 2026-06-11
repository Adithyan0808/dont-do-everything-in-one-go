import { useMemo, useState } from 'react';
import { usePendingApprovals, useBulkApprove } from '../hooks/useEligibilityQueries';
import ApprovalQueueTable from '../components/ApprovalQueueTable';
import BulkApprovalToolbar from '../components/BulkApprovalToolbar';

export default function ApprovalsPage() {
  const { data: approvals = [], isLoading } = usePendingApprovals();
  const [selected, setSelected] = useState([]);
  const bulk = useBulkApprove();

  const kpis = useMemo(
    () => ({
      pending: approvals.length,
      highPriority: approvals.filter((a) => a.priority === 'High').length,
    }),
    [approvals],
  );

  const toggleSelect = (id) => setSelected((s) => (s.includes(id) ? s.filter((x) => x !== id) : [...s, id]));
  const selectAll = () => setSelected((s) => (s.length === approvals.length ? [] : approvals.map((a) => a.registrationId)));
  const clearSelection = () => setSelected([]);

  return (
    <div className="space-y-5">
      <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
        <h1 className="text-2xl font-semibold text-slate-950">Pending Approvals</h1>
        <p className="mt-1 text-sm text-slate-500">Enterprise approval center for eligibility decisions.</p>
        <div className="mt-4 flex gap-4">
          <div className="rounded-card bg-slate-50 p-3">Pending: <strong>{kpis.pending}</strong></div>
          <div className="rounded-card bg-slate-50 p-3">High Priority: <strong>{kpis.highPriority}</strong></div>
        </div>

        <div className="mt-4">
          <BulkApprovalToolbar selectedIds={selected} onClear={clearSelection} />
        </div>
      </section>

      <section>
        <ApprovalQueueTable
          approvals={approvals}
          loading={isLoading}
          selectedIds={selected}
          onToggleSelect={toggleSelect}
          onSelectAll={selectAll}
        />
      </section>
    </div>
  );
}
