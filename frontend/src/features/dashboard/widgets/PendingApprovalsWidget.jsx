import { Check, Eye, ShieldCheck, X } from 'lucide-react';
import { Link } from 'react-router-dom';
import EmptyState from '../../../components/common/EmptyState';
import DashboardErrorState from '../components/DashboardErrorState';
import WidgetSkeleton from '../components/WidgetSkeleton';

function PendingApprovalsWidget({ query }) {
  if (query.isLoading) return <WidgetSkeleton title="Approval Center" />;
  if (query.isError) return <DashboardErrorState error={query.error} onRetry={query.refetch} />;

  const approvals = query.data || [];

  return (
    <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
      <div className="flex flex-col gap-3 sm:flex-row sm:items-start sm:justify-between">
        <div>
          <h2 className="text-lg font-semibold text-slate-950">Approval Center</h2>
          <p className="text-sm text-slate-500">Pending registrations, eligibility reviews, and escalations</p>
        </div>
        <button
          type="button"
          className="inline-flex items-center justify-center gap-2 rounded-md border border-slate-300 px-3 py-2 text-sm font-medium text-slate-700"
        >
          <ShieldCheck className="h-4 w-4" />
          Bulk Approve
        </button>
      </div>

      {approvals.length === 0 ? (
        <div className="mt-4">
          <EmptyState title="No Pending Approvals" description="All approval queues are clear." />
        </div>
      ) : (
        <div className="mt-4 space-y-3">
          {approvals.map((approval) => (
            <article key={approval.id} className="rounded-card border border-slate-200 p-4">
              <div className="flex flex-col gap-3 md:flex-row md:items-center md:justify-between">
                <div className="min-w-0">
                  <p className="truncate text-sm font-semibold text-slate-900">{approval.candidate}</p>
                  <p className="mt-1 truncate text-sm text-slate-500">{approval.drive}</p>
                  <div className="mt-2 flex flex-wrap items-center gap-2">
                    <span className="rounded-full bg-indigo-50 px-2 py-1 text-xs font-medium text-primary">{approval.type}</span>
                    <span className="rounded-full bg-amber-50 px-2 py-1 text-xs font-medium text-amber-700">Waiting {approval.age}</span>
                  </div>
                </div>
                <div className="flex items-center gap-2">
                  <button type="button" className="rounded-md bg-success p-2 text-white" aria-label={`Approve ${approval.candidate}`}>
                    <Check className="h-4 w-4" />
                  </button>
                  <button type="button" className="rounded-md bg-danger p-2 text-white" aria-label={`Reject ${approval.candidate}`}>
                    <X className="h-4 w-4" />
                  </button>
                  <Link to={`/eligibility/${approval.id}`} className="rounded-md border border-slate-300 p-2 text-slate-600" aria-label={`View ${approval.candidate}`}>
                    <Eye className="h-4 w-4" />
                  </Link>
                </div>
              </div>
            </article>
          ))}
        </div>
      )}
    </section>
  );
}

export default PendingApprovalsWidget;
