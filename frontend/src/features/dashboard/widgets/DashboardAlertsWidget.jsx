import { AlertOctagon } from 'lucide-react';
import EmptyState from '../../../components/common/EmptyState';
import DashboardErrorState from '../components/DashboardErrorState';
import WidgetSkeleton from '../components/WidgetSkeleton';
import { ALERT_SEVERITY_STYLES } from '../constants/dashboardConstants';

function DashboardAlertsWidget({ query }) {
  if (query.isLoading) return <WidgetSkeleton title="Alerts" />;
  if (query.isError) return <DashboardErrorState error={query.error} onRetry={query.refetch} />;

  const alerts = query.data || [];

  return (
    <section className="rounded-card border border-slate-200 bg-white p-5 shadow-sm">
      <div className="flex items-start justify-between gap-3">
        <div>
          <h2 className="text-lg font-semibold text-slate-950">Alerts & Risk Monitoring</h2>
          <p className="text-sm text-slate-500">Items requiring immediate operational attention</p>
        </div>
        <AlertOctagon className="h-5 w-5 text-slate-400" aria-hidden="true" />
      </div>

      {alerts.length === 0 ? (
        <div className="mt-4">
          <EmptyState title="No Alerts" description="No operational risks are currently detected." />
        </div>
      ) : (
        <div className="mt-4 space-y-3">
          {alerts.map((alert) => (
            <article key={alert.id} className={`rounded-card border p-4 ${ALERT_SEVERITY_STYLES[alert.severity] || ALERT_SEVERITY_STYLES.Low}`}>
              <div className="flex items-start justify-between gap-3">
                <div>
                  <span className="text-xs font-bold uppercase">{alert.severity}</span>
                  <h3 className="mt-1 text-sm font-semibold">{alert.title}</h3>
                  <p className="mt-1 text-sm opacity-90">{alert.message}</p>
                </div>
              </div>
            </article>
          ))}
        </div>
      )}
    </section>
  );
}

export default DashboardAlertsWidget;
