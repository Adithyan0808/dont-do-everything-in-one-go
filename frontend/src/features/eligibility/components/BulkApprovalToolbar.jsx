import { useBulkApprove } from '../hooks/useEligibilityQueries';

export default function BulkApprovalToolbar({ selectedIds = [], onClear }) {
  const bulk = useBulkApprove();

  const handleBulkApprove = () => {
    if (!selectedIds.length) return;
    bulk.mutate({ ids: selectedIds }, { onSuccess: () => onClear?.() });
  };

  const handleExport = () => {
    if (!selectedIds.length) return;
    const payload = JSON.stringify(selectedIds, null, 2);
    const blob = new Blob([payload], { type: 'application/json' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = `approvals-selected-${Date.now()}.json`;
    a.click();
    URL.revokeObjectURL(url);
  };

  return (
    <div className="flex items-center justify-between gap-2">
      <div className="flex items-center gap-3">
        <div className="text-sm text-slate-600">Selected: <strong>{selectedIds.length}</strong></div>
        <button type="button" className="rounded-md bg-emerald-600 px-3 py-2 text-sm text-white" onClick={handleBulkApprove} disabled={!selectedIds.length}>Approve Selected</button>
        <button type="button" className="rounded-md border px-3 py-2 text-sm" onClick={handleExport} disabled={!selectedIds.length}>Export Selected</button>
      </div>

      <div className="text-sm text-slate-500">Use selection to perform bulk operations.</div>
    </div>
  );
}
