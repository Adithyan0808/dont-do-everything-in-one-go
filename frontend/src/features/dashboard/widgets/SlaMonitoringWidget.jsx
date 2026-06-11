import { AlertTriangle, TimerReset } from 'lucide-react';
import DashboardErrorState from '../components/DashboardErrorState';
import ProgressBar from '../components/ProgressBar';
import WidgetSkeleton from '../components/WidgetSkeleton';
import { getSlaTone } from '../utils/dashboardFormatters';

const toneClasses = {
  success: 'border-emerald-200 bg-emerald-50 text-emerald-800',
  warning: 'border-amber-200 bg-amber-50 text-amber-800',
  danger: 'border-red-200 bg-red-50 text-red-800',
};

function SlaMonitoringWidget({ query }) {
  if (query.isLoading) return <WidgetSkeleton title="SLA Monitoring" />;
  if (query.isError) return <DashboardErrorState error={query.error} onRetry={query.refetch} />;

  const sla = query.data || {};
  const tone = getSlaTone(sla.compliance);

  return (
    <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
      <div className="flex items-start justify-between gap-3">
        <div>
          <h2 className="text-lg font-semibold text-slate-950">SLA Monitoring</h2>
          <p className="text-sm text-slate-500">Operational response and delivery targets</p>
        </div>
        <TimerReset className="h-5 w-5 text-slate-400" aria-hidden="true" />
      </div>

      <div className={`mt-5 rounded-card border p-4 ${toneClasses[tone]}`}>
        <div className="flex items-center justify-between gap-3">
          <div>
            <p className="text-sm font-medium">Overall Compliance</p>
            <p className="mt-1 text-3xl font-semibold">{sla.compliance ?? 0}%</p>
          </div>
          <AlertTriangle className="h-6 w-6" aria-hidden="true" />
        </div>
        <div className="mt-4">
          <ProgressBar current={sla.compliance ?? 0} target={100} label="SLA compliance" />
        </div>
      </div>

      <dl className="mt-4 grid grid-cols-3 gap-3 text-sm">
        <div className="rounded-card border border-slate-200 p-3">
          <dt className="text-slate-500">Breached</dt>
          <dd className="mt-1 text-lg font-semibold text-slate-950">{sla.breachedCount ?? 0}</dd>
        </div>
        <div className="rounded-card border border-slate-200 p-3">
          <dt className="text-slate-500">Response</dt>
          <dd className="mt-1 text-lg font-semibold text-slate-950">{sla.averageResponseTime ?? '0m'}</dd>
        </div>
        <div className="rounded-card border border-slate-200 p-3">
          <dt className="text-slate-500">Approval</dt>
          <dd className="mt-1 text-lg font-semibold text-slate-950">{sla.averageApprovalTime ?? '0m'}</dd>
        </div>
      </dl>
    </section>
  );
}

export default SlaMonitoringWidget;
