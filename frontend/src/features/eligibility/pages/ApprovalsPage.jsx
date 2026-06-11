import { useMemo, useState } from 'react';
import toast from 'react-hot-toast';
import { usePendingApprovals, useBulkApprove, useApproveEligibility, useRejectEligibility } from '../hooks/useEligibilityQueries';
import ApprovalQueueTable from '../components/ApprovalQueueTable';
import BulkApprovalToolbar from '../components/BulkApprovalToolbar';
import ApproveEligibilityModal from '../components/ApproveEligibilityModal';
import RejectEligibilityModal from '../components/RejectEligibilityModal';

export default function ApprovalsPage() {
  const { data: approvals = [], isLoading } = usePendingApprovals();
  const [selected, setSelected] = useState([]);
  const bulk = useBulkApprove();
  const approveMutation = useApproveEligibility();
  const rejectMutation = useRejectEligibility();

  const [approveOpen, setApproveOpen] = useState(false);
  const [rejectOpen, setRejectOpen] = useState(false);
  const [currentRegistration, setCurrentRegistration] = useState(null);

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

  const openApprove = (registration) => {
    setCurrentRegistration(registration);
    setApproveOpen(true);
  };

  const openReject = (registration) => {
    setCurrentRegistration(registration);
    setRejectOpen(true);
  };

  const onConfirmApprove = (payload) => {
    if (!currentRegistration) return;
    approveMutation.mutate({ registrationId: currentRegistration.registrationId, payload: { approver: payload.approver, comments: payload.comments } });
    setApproveOpen(false);
    toast.success('Approval submitted');
  };

  const onConfirmReject = (payload) => {
    if (!currentRegistration) return;
    rejectMutation.mutate({ registrationId: currentRegistration.registrationId, payload: { approver: payload.approver || 'current.user', reason: payload.reason, comments: payload.comments } });
    setRejectOpen(false);
    toast.success('Rejection submitted');
  };

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
          onOpenApprove={openApprove}
          onOpenReject={openReject}
        />
      </section>

      <ApproveEligibilityModal open={approveOpen} onClose={() => setApproveOpen(false)} registration={currentRegistration} onConfirm={onConfirmApprove} />
      <RejectEligibilityModal open={rejectOpen} onClose={() => setRejectOpen(false)} registration={currentRegistration} onConfirm={onConfirmReject} />
    </div>
  );
}
