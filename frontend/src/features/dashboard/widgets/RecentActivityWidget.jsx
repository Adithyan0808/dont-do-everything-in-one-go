import { Activity, CheckCircle2, Clock, XCircle } from 'lucide-react';
import EmptyState from '../../../components/common/EmptyState';
import DashboardErrorState from '../components/DashboardErrorState';
import WidgetSkeleton from '../components/WidgetSkeleton';

const activityIcons = {
  'Registration Approved': CheckCircle2,
  'Registration Rejected': XCircle,
  'Assessment Passed': CheckCircle2,
  'Assessment Failed': XCircle,
  'Voucher Assigned': Activity,
  'Voucher Redeemed': CheckCircle2,
};

function RecentActivityWidget({ query }) {
  if (query.isLoading) return <WidgetSkeleton title="Recent Activity" />;
  if (query.isError) return <DashboardErrorState error={query.error} onRetry={query.refetch} />;

  const events = query.data || [];

  return (
    <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
      <div className="flex items-start justify-between gap-3">
        <div>
          <h2 className="text-lg font-semibold text-slate-950">Recent Activity</h2>
          <p className="text-sm text-slate-500">Last 20 operational events across drives</p>
        </div>
        <Clock className="h-5 w-5 text-slate-400" aria-hidden="true" />
      </div>

      {events.length === 0 ? (
        <div className="mt-4">
          <EmptyState title="No Recent Activity" description="Operational events will appear here as teams work through the certification flow." />
        </div>
      ) : (
        <ol className="mt-5 space-y-4" aria-label="Recent dashboard activity">
          {events.slice(0, 20).map((event) => {
            const Icon = activityIcons[event.type] || Activity;
            return (
              <li key={event.id} className="relative flex gap-3">
                <span className="mt-0.5 flex h-8 w-8 shrink-0 items-center justify-center rounded-full bg-indigo-50 text-primary">
                  <Icon className="h-4 w-4" aria-hidden="true" />
                </span>
                <div className="min-w-0 flex-1 border-b border-slate-100 pb-4">
                  <div className="flex flex-wrap items-center justify-between gap-2">
                    <p className="truncate text-sm font-semibold text-slate-900">{event.type}</p>
                    <span className="text-xs text-slate-500">{event.timestamp}</span>
                  </div>
                  <p className="mt-1 text-sm text-slate-600">
                    <span className="font-medium">{event.user}</span> on {event.entity}
                  </p>
                  <span className="mt-2 inline-flex rounded-full bg-slate-100 px-2 py-1 text-xs font-medium text-slate-700">
                    {event.status}
                  </span>
                </div>
              </li>
            );
          })}
        </ol>
      )}
    </section>
  );
}

export default RecentActivityWidget;
